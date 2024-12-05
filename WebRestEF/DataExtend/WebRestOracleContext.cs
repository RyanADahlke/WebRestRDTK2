using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Oracle.ManagedDataAccess.Client;
using WebRestEF.EF.Models;

namespace WebRestEF.EF.Data;

public partial class WebRestOracleContext : DbContext
{
    public string LoggedInUserId { get; set; }
    public string DefaultSchema { get; set; } = "UD_RADAHLKE";

    public void SetUserID(string UserID)
    {
        this.LoggedInUserId = UserID;
        var user_id_in = new OracleParameter("p_User_ID", OracleDbType.Varchar2, UserID, ParameterDirection.Input);
        try
        {
            this.Database.ExecuteSqlRaw($"BEGIN {DefaultSchema}.PKG_SET_CONTEXT.SET_USER_ID({0}); END;", user_id_in);
        }
        catch (Exception ex)
        {           
            throw;
        }
    }


    private void HandleSaveChanges()
    {
        ChangeTracker.DetectChanges();

        foreach (var entry in ChangeTracker.Entries())
        {
            var entryName = entry.Metadata;

            if (entry.State == EntityState.Added)
            {
                foreach (var property in entry.Members)
                {
                    if (property.Metadata.PropertyInfo.CustomAttributes.Any(a => a.AttributeType.Name.ToUpper() == "KEYATTRIBUTE"))
                    {
                        if (property.CurrentValue == null)
                        {
                            property.CurrentValue = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        }                        
                    }
                    //if ((property.Metadata.Name.ToUpper().EndsWith("CRTDDT")) || (property.Metadata.Name.ToUpper().EndsWith("UPDTDT")))
                    //{
                    //    property.CurrentValue = DateTime.UtcNow;
                    //}
                    //if ((property.Metadata.Name.ToUpper().EndsWith("CRTDID")) || (property.Metadata.Name.ToUpper().EndsWith("UPDTID")))
                    //{
                    //    property.CurrentValue = LoggedInUserId;
                    //}
                }
            }

            //if (entry.State == EntityState.Modified)
            //{
            //    foreach (var property in entry.Members)
            //    {
            //        if (property.Metadata.Name.ToUpper().Contains("UPDTDT"))
            //        {
            //            property.CurrentValue = DateTime.UtcNow;
            //        }
            //        if (property.Metadata.Name.ToUpper().Contains("UPDTID"))
            //        {
            //            property.CurrentValue = LoggedInUserId;
            //        }
            //    }
            //}
        }
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        HandleSaveChanges();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }
    public override int SaveChanges()
    {
        HandleSaveChanges();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        HandleSaveChanges();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        HandleSaveChanges();
        return base.SaveChangesAsync(cancellationToken);
    }
}
