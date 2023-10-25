﻿using CCA.API.Controllers;
using CCA.API.Data;
using CCA.API.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace CCA.API.Repository
{
    public class CCARepository
    {

        private static object databaseLock = new object();
        public static DbContextOptions<DataContext> GetConfiguration()
        {
            return new DbContextOptionsBuilder<DataContext>()
           .UseSqlite( "DataSource=cca.db;Cache=Shared" )
           .Options;
        }

        #region Crud CurrentAccountStatement

        public static async Task<CurrentAccountStatement> Create( CurrentAccountStatement model )
        {

            try
            {
                lock (databaseLock)
                {
                    using (var dbContext = new DataContext( GetConfiguration() ))
                    {
                        dbContext.AccountStatement.Add( model );
                        dbContext.SaveChangesAsync();
                        dbContext.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return model;
        }

        public static async Task<CurrentAccountStatement> Update( CurrentAccountStatement model )
        {

            try
            {
                lock (databaseLock)
                {
                    using (var dbContext = new DataContext( GetConfiguration() ))
                    {
                        var Statement = dbContext.AccountStatement.Where( x => x.Id == model.Id ).FirstOrDefault();
                        if (Statement != null)
                        {
                            Statement.Description = model.Description;
                            Statement.StartDate = model.StartDate;
                            Statement.Value = model.Value;
                            Statement.Loose = model.Loose;
                            Statement.State = model.State;
                            dbContext.AccountStatement.Update( Statement );
                            dbContext.SaveChangesAsync();
                            dbContext.Dispose();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return model;
        }

        public static async Task<CurrentAccountStatement> Delete( int id )
        {
            var Statement = new CurrentAccountStatement();

            try
            {
                lock (databaseLock)
                {
                    using (var dbContext = new DataContext( GetConfiguration() ))
                    {
                        Statement = dbContext.AccountStatement.Where( x => x.Id == id ).FirstOrDefault();
                        Statement.State = false;
                        dbContext.AccountStatement.Update( Statement );
                        dbContext.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Statement;
        }

        public static async Task<CurrentAccountStatement> GetById( int Id )
        {
            var Statement = new CurrentAccountStatement();

            using (var dbContext = new DataContext( GetConfiguration() ))
            {
                Statement = await dbContext.AccountStatement
                    .AsNoTracking()
                    .Where( x => x.Id == Id && x.State)
                    .FirstOrDefaultAsync();
                dbContext.Dispose();
            }

            return Statement;
        }

        public static async Task<List<CurrentAccountStatement>> GetAll()
        {
            var startements = new List<CurrentAccountStatement>();

            using (var dbContext = new DataContext( GetConfiguration() ))
            {
                startements = await dbContext
                .AccountStatement
                .AsNoTracking()
                .Where( x => x.State )
                .ToListAsync();

                dbContext.Dispose();
            }

            return startements;
        }

        #endregion

    }
}
