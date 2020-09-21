﻿using Destiny.Core.Flow.Model.IdentityServer4;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Destiny.Core.Flow.Model.EntityConfigurations.IdentityServer4.Resources
{
    public class ApiResourceConfiguration : EntityMappingConfiguration<ApiResource, Guid>
    {
        public override void Map(EntityTypeBuilder<ApiResource> b)
        {
            b.HasKey(o => o.Id);
            b.ToTable("ApiResource");
        }
    }
}