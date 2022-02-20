using System;
using System.Collections.Generic;
using System.Text.Json;
using App.Controllers.Api;
using App.Models.Entry;
using App.Services;
using Xunit;
using Xunit.Abstractions;

namespace WebApiTests.Controllers;

public class EntryGetTest : ApiTestCase
{
    private readonly ITestOutputHelper _output;
    private readonly EntryController _controller;
    private readonly Guid _entryId;
    private readonly List<BaseEntry> _entries;

    public EntryGetTest(ITestOutputHelper output)
    {
        _output = output;
        _controller = new EntryController(Db, new EntryService());
        _entryId = Guid.NewGuid();
        _entries = new List<BaseEntry>()
        {
            new Person() {Id = _entryId, Name = "Tom"},
            new Company() {Name = "ABCDE@COMPANY"},
        };
    }

    private async void AddEntries()
    {
        _output.WriteLine("Tom Id: " + _entryId);
        Db.Entries.AddRange(_entries);
        await Db.SaveChangesAsync();
    }

    [Fact]
    public async void GetAll()
    {
        var resp = await _controller.Get();
        var emptyEntries = resp.Value;
        if (emptyEntries != null) Assert.Empty(emptyEntries);

        AddEntries();
        resp = await _controller.Get();
        Assert.Equal(_entries.Count, resp.Value.Count);
    }

    [Fact]
    public async void GetSingle()
    {
        AddEntries();

        var entry = (await _controller.Get(_entryId)).Value;
        _output.WriteLine(entry.Name);
        Assert.Equal("Tom", entry.Name);
        Assert.IsAssignableFrom<BaseEntry>(entry);
    }

    [Fact]
    public async void CreatePerson()
    {
        var resp = (await _controller.CreatePerson(new PersonDto()
        {
            Name = "Jerry",
            Reputation = 3,
        }));

        _output.WriteLine(resp.Value.Name);
    }
}