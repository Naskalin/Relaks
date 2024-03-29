﻿using System.Globalization;
using BootstrapBlazor.Components;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Relaks.Interfaces;
using Relaks.Managers;
using Relaks.Models;
using Relaks.Models.Misc;
using Relaks.Models.Store;
using Relaks.Models.StructureModels;
using Relaks.Utils;
using Relaks.Validators;
using Relaks.Validators.AppFileValidators;
using Relaks.Validators.EntryInfoValidators;
using Relaks.Validators.EntryTagValidators;
using Relaks.Validators.EntryValidators;
using Relaks.Validators.FinancialValidators;
using Relaks.Validators.StructureValidators;
using Relaks.Views.Pages.EntryFinancials.ViewModels;
using Relaks.Views.Pages.FinancialTransactionCategories.ViewModels;

namespace Relaks;

public static class RegisterServices
{
    public static void RegisterManagers(this IServiceCollection services)
    {
        // services.AddSingleton<RelaksConfigManager>();
        services.AddSingleton<AppOperation>();
        
        services.AddScoped<EntryManager>();
        services.AddScoped<AppFileManager>();
        services.AddScoped<FinancialManager>();
        services.AddSignalR(e => e.MaximumReceiveMessageSize = null);
    }

    public static void RegisterValidators(this IServiceCollection services)
    {
        ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
        services.AddScoped<IValidator<EPerson>, BaseEntryValidator>();
        services.AddScoped<IValidator<ECompany>, BaseEntryValidator>();
        services.AddScoped<IValidator<EProject>, BaseEntryValidator>();
        services.AddScoped<IValidator<EntryRelationRequest>, EntryRelationValidator>();

        services.AddScoped<IValidator<EiDate>, EiDateValidator>();
        services.AddScoped<IValidator<EiPhone>, EiPhoneValidator>();
        services.AddScoped<IValidator<EiUrl>, EiUrlValidator>();
        services.AddScoped<IValidator<EiEmail>, EiEmailValidator>();
        services.AddScoped<IValidator<EiDatasetRequest>, EiDatasetRequestValidator>();
        services.AddScoped<IValidator<DatasetModel>, DatasetModelValidator>();
        services.AddScoped<IValidator<DatasetTemplateRequest>, DatasetTemplateRequestValidator>();

        services.AddScoped<IValidator<EntryFileTag>, EntryFileTagValidator>();
        services.AddScoped<IValidator<EntryFileCategory>, EntryFileCategoryValidator>();
        services.AddScoped<IValidator<BaseFileRequest>, BaseFileRequestValidator>();
        
        services.AddScoped<IValidator<StructureItem>, StructureItemValidator>();
        services.AddScoped<IValidator<StructureGroup>, StructureGroupValidator>();
        
        services.AddScoped<IValidator<AppFirstRunRequest>, AppFirstRunRequestValidator>();
        
        services.AddScoped<IValidator<EntryTag>, EntryTagValidator>();
        services.AddScoped<IValidator<EntryTagCategory>, EntryTagCategoryValidator>();
        services.AddScoped<IValidator<EntryTagTitle>, EntryTagTitleValidator>();
        
        services.AddScoped<IValidator<FinancialAccountCategoryRequest>, FinancialAccountCategoryRequestValidator>();
        services.AddScoped<IValidator<FinancialAccountRequest>, FinancialAccountRequestValidator>();
        services.AddScoped<IValidator<EntryFinancialTransactionRequest>, EntryFinancialTransactionRequestValidator>();
        services.AddScoped<IValidator<AccountFinancialTransactionRequest>, AccountFinancialTransactionRequestValidator>();
        services.AddScoped<IValidator<FinancialTransactionItemRequest>, FinancialTransactionItemRequestValidator>();
        services.AddScoped<IValidator<FinancialTransactionCategoryRequest>, FinancialTransactionCategoryRequestValidator>();

        // services.AddScoped<IValidator<EntryInfoCreateRequest>, EntryInfoCreateRequestValidator>();
    }

    public static void RegisterLocalization(this IServiceCollection services)
    {
        services.AddLocalization(options => options.ResourcesPath = Path.Combine("src", "Resources"));
        services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = new[] {"ru-RU", "en-US"};
            options
                .SetDefaultCulture(supportedCultures.First())
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);
        });
    }
}