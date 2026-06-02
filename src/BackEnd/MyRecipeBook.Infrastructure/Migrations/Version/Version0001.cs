using FluentMigrator;

namespace MyRecipeBook.Infrastructure.Migrations.Version;

[Migration(DataBaseVersion.TABELA_USUARIO,"Crate Table to Save User's Information")]
public class Version0001 : VersionBase
{
    public override void Up()
    {
        CreateTable("Users")
              .WithColumn("Nome").AsString(255).NotNullable()
              .WithColumn("Email").AsString(255).NotNullable()
              .WithColumn("Password").AsString(255).NotNullable();
    }
}
