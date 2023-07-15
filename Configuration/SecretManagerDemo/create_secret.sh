# add a user secrets ID to the project
# In Visual Studio, right click a project and select Manage User Secrets
dotnet user-secrets init

# set some secret
dotnet user-secrets set "Movies:ConnectionString" "blah blah blah"


