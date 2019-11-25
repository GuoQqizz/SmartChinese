using AbhsChinese.Repository.DBContext;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;

namespace AbhsChinese.Bll
{
    public class AbhsDbContextFactory : IDisposable
    {
        private AbhsChineseDBContext dbContext;
        public AbhsChineseDBContext DBContext
        {
            set
            {
                dbContext = value;
            }
            get
            {
                if (dbContext == null)
                {
                    dbContext = new AbhsChineseDBContext();
                }
                return dbContext;
            }
        }

        private AbhsChineseDBContextPart1 dbContextPart1;
        public AbhsChineseDBContextPart1 DBContextPart1
        {
            set
            {
                dbContextPart1 = value;
            }
            get
            {
                if (dbContextPart1 == null)
                {
                    dbContextPart1 = new AbhsChineseDBContextPart1();
                }
                return dbContextPart1;
            }
        }

        public static AbhsDbContextFactory CreateDbContext()
        {
            //AbhsChineseDBContext DBContext1 = new AbhsChineseDBContext();
            //var objectContext = ((IObjectContextAdapter)DBContext1).ObjectContext;
            //var mappingCollection = (StorageMappingItemCollection)objectContext.MetadataWorkspace.GetItemCollection(DataSpace.CSSpace);
            //mappingCollection.GenerateViews(new List<EdmSchemaError>());
            return new AbhsDbContextFactory();
        }

        public void Dispose()
        {
            if (dbContext != null)
            {
                dbContext.Dispose();
            }
        }
    }
}