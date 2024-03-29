﻿using Relaks.Interfaces;
using Relaks.Models;

namespace Relaks.Mappers;

public static class EntryInfoMapper
{
    public static void MapEntryInfo(this IEntryInfo from, IEntryInfo to)
    {
        to.Title = from.Title?.Trim();
        to.IsFavorite = from.IsFavorite;
    }

    public static string ToFtsBody(this BaseEntryInfo eInfo)
    {
        var arr = new List<string?>() {eInfo.Title};

        switch (eInfo.Discriminator)
        {
            case nameof(EiEmail):
                arr.Add(((EiEmail) eInfo).Email);
                break;
            case nameof(EiPhone):
                arr.Add(((EiPhone) eInfo).Number);
                break;
            case nameof(EiUrl):
                arr.Add(((EiUrl) eInfo).Url);
                break;
            case nameof(EiDataset):
                foreach (var group in ((EiDataset) eInfo).Dataset.Groups.Where(group => !group.Items.All(x => string.IsNullOrEmpty(x.Value))))
                {
                    arr.Add(group.Title);
                    foreach (var item in group.Items.Where(item => !string.IsNullOrEmpty(item.Value)))
                    {
                        arr.Add(item.Key);
                        arr.Add(item.Value);
                    }
                }
                break;
        }
        
        arr.Add(eInfo.DeletedReason);

        return string.Join(" ", arr.Where(x => !string.IsNullOrEmpty(x)));
    }

    public static void MapTo(this EiDatasetRequest req, EiDataset model)
    {
        req.MapDataset(model);
        req.MapSoftDeleted(model);
        model.Title = req.Title;
        model.IsFavorite = req.IsFavorite;
    }
    
    public static void MapTo(this EiDataset model, EiDatasetRequest req)
    {
        model.MapDataset(req);
        model.MapSoftDeleted(req);
        req.Title = model.Title;
        req.IsFavorite = model.IsFavorite;
    }

    public static void MapTo(this DatasetTemplate template, EiDatasetRequest model)
    {
        var dataset = template.Dataset;
        dataset.Groups.ForEach(group =>
        {
            group.Id = Guid.NewGuid().ToString();
            group.Items.ForEach(item => item.Id = Guid.NewGuid().ToString());
        });

        model.Title = template.Title;
        model.Dataset = dataset;
    }
}