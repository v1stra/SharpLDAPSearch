# SharpLDAPSearch
C# .NET Assembly to perform LDAP Queries


## Usage
Positional arguments for the ldap query and comma-separated attributes to return from the query

If no attributes are specified, all attributes are returned for each object in the query's results 
```
execute-assembly /path/to/SharpLDAPSearch.exe /filter:"ldap query" /properties:"attributes,to,return" /ldap:"LDAP://DC=test,DC=local"
```

## Example
```
SharpLDAPSearch.exe /filter:"(&(objectClass=user)(cn=*svc*))" /properties:"samaccountname" /ldap:"LDAP://DC=test,DC=local"
```
### Output
```
svc-admin
svc-ops
spsvcuser
```
