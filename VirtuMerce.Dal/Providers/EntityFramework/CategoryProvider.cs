﻿using Microsoft.EntityFrameworkCore;
using VirtuMerce.Dal.Entities;
using VirtuMerce.Dal.Providers.Abstract;
using VirtuMerce.Dal.Providers.EntityFramework.Abstract;

namespace VirtuMerce.Dal.Providers.EntityFramework;

public class CategoryProvider : BaseProvider<CategoryEntity>, ICategoryProvider
{
    private readonly ApplicationContext _applicationContext;

    public CategoryProvider(ApplicationContext applicationContext) : base(applicationContext)
    {
        _applicationContext = applicationContext;
    }
}
