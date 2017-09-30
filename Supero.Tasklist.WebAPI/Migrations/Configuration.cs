namespace Supero.Tasklist.WebAPI.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Supero.Tasklist.Contexts.SuperoTasklistWebAPIContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Supero.Tasklist.Contexts.SuperoTasklistWebAPIContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Task.AddOrUpdate(
                p => p.CD_TASK,
                new Models.Task { CD_TASK = new Guid("A0FEC507-FC0D-4489-9584-713A9637A34E"), DS_TITLE = "Detalhar tecnicamente o sistema X", DS_TASK = "Documentar tecnicamente os requisitos do sistema X.", DT_CREATION = DateTime.Now, ST_FINISHED = false, ST_REMOVED = false },
                new Models.Task { CD_TASK = new Guid("FB2BA2C4-C232-4D38-B03A-2A675C18EF04"), DS_TITLE = "Modelar a tabela Task", DS_TASK = "Criar o modelo entidade relacionamento da tabela de atividades no sistema X.", DT_CREATION = DateTime.Now, ST_FINISHED = false, ST_REMOVED = false },
                new Models.Task { CD_TASK = new Guid("407A010B-5FA6-435D-B072-9AA00B072F43"), DS_TITLE = "Diagramar o sistema X", DS_TASK = "Criar um diagrama com todos os pontos principais do sistema X.", DT_CREATION = DateTime.Now, ST_FINISHED = false, ST_REMOVED = false }
            );
        }
    }
}
