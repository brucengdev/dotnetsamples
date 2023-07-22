# depending on the env var prefixes, additional database provider configuration will
# be added automatically by the ASP.NET framework

export CUSTOMCONNSTR_MyCustomDb=Foo
export MYSQLCONNSTR_MyMySqlDb=Foo
export SQLCONNSTR_MySqlDb=Foo
export SQLAZURECONNSTR_MySQLAzureDb=Foo
dotnet run