﻿using Destiny.Core.Flow.Model.IdentityServer4;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Destiny.Core.Flow.Model.EntityConfigurations.IdentityServer4.Clients
{
    public class ClientPropertyConfiguration : EntityMappingConfiguration<ClientProperty, Guid>
    {
        public override void Map(EntityTypeBuilder<ClientProperty> b)
        {
            b.HasKey(o => o.Id);
            b.ToTable("ClientProperty");
        }
    }
}