using Arztpraxis.Data;
using Arztpraxis.Data.Models.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Arztpraxis.ViewModels
{
    public class TreatmentListViewModel
    {
        public List<TreatmentInfo> Treatments { get; private set; }

        public IDbContextFactory<DoctorsOfficeDbContext> dbContextFactory;

        public TreatmentListViewModel()
        {
            this.dbContextFactory = App.host.Services.GetService<IDbContextFactory<DoctorsOfficeDbContext>>()
                ?? throw new Exception("No Database connection. Cannot load Treatments.");

            this.LoadTreatments();
        }

        [MemberNotNull(nameof(this.Treatments))]
        public void LoadTreatments()
        {
            if (App.Current.CurrentUser == null)
            {
                throw new Exception("No User is logged in. Cannot load Treatments.");
            }

            using DoctorsOfficeDbContext dbContext = dbContextFactory.CreateDbContext();
            this.Treatments =
                dbContext.TreatmentInfos
                //.Where(t => t.CreatedById == App.Current.CurrentUser.Id)
                .ToList();
        }
    }
}
