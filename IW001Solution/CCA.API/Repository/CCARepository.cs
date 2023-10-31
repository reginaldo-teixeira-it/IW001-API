using CCA.API.Controllers;
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

        public static async Task<CurrentAccountStatementModel> Create( CurrentAccountStatementModel model )
        {

            try
            {
                lock (databaseLock)
                {
                    using (var dbContext = new DataContext( GetConfiguration() ))
                    {
                        dbContext.CurrentAccountStatement.Add( model );
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

        public static async Task<CurrentAccountStatementModel> Update( CurrentAccountStatementModel model )
        {

            try
            {
                lock (databaseLock)
                {
                    using (var dbContext = new DataContext( GetConfiguration() ))
                    {
                        var Statement = dbContext.CurrentAccountStatement.Where( x => x.Id == model.Id ).FirstOrDefault();
                        if (Statement != null)
                        {
                            Statement.Description = model.Description;
                            Statement.StartDate = model.StartDate;
                            Statement.Value = model.Value;
                            Statement.Loose = model.Loose;
                            Statement.State = model.State;
                            dbContext.CurrentAccountStatement.Update( Statement );
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

        public static async Task<CurrentAccountStatementModel> Delete( int id )
        {
            var Statement = new CurrentAccountStatementModel();

            try
            {
                lock (databaseLock)
                {
                    using (var dbContext = new DataContext( GetConfiguration() ))
                    {
                        Statement = dbContext.CurrentAccountStatement.Where( x => x.Id == id ).FirstOrDefault();
                        dbContext.CurrentAccountStatement.Remove( Statement );
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

        public static async Task<CurrentAccountStatementModel> GetById( int Id )
        {
            var Statement = new CurrentAccountStatementModel();

            using (var dbContext = new DataContext( GetConfiguration() ))
            {
                Statement = await dbContext.CurrentAccountStatement
                    .AsNoTracking()
                    .Where( x => x.Id == Id )
                    .FirstOrDefaultAsync();
                dbContext.Dispose();
            }

            return Statement;
        }

        public static async Task<List<CurrentAccountStatementModel>> GetAll()
        {
            var startements = new List<CurrentAccountStatementModel>();

            using (var dbContext = new DataContext( GetConfiguration() ))
            {
                startements = await dbContext
                .CurrentAccountStatement
                .AsNoTracking()
                .ToListAsync();

                dbContext.Dispose();
            }

            return startements;
        }

        public static async Task<CurrentAccountStatementModel> Cancel( int id )
        {
            var Statement = new CurrentAccountStatementModel();

            try
            {
                lock (databaseLock)
                {
                    using (var dbContext = new DataContext( GetConfiguration() ))
                    {
                        Statement = dbContext.CurrentAccountStatement.Where( x => x.Id == id ).FirstOrDefault();
                        Statement.State = false;
                        dbContext.CurrentAccountStatement.Update( Statement );
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

        #endregion

    }
}
