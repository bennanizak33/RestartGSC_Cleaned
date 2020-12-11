using McDonalds.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace McDonalds.Data.Context
{
	public class McDonaldsContext : DbContext
	{
        #region Constructors

        public McDonaldsContext()
            : base("DatabaseConnectionString")
        {
            this.Configuration.ValidateOnSaveEnabled = true;
            this.Configuration.AutoDetectChangesEnabled = true;
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
        }

        #endregion Constructors

        #region Properties

        public DbSet<Restaurant> Restaurants
        {
            get;
            set;
        }

        public DbSet<ServerEvent> ServerEvents
        {
            get;
            set;
        }

        public DbSet<Holyday> Holydays
        {
            get;
            set;
        }

        #endregion Properties
    }
}