using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;

namespace SharpLDAPSearch
{
    class Program
    { 
        static void Main(string[] args)
        {

			string ldap = Util.getArgsByName("ldap", args);
			string filter = Util.getArgsByName("filter", args);
			string props = Util.getArgsByName("properties", args);

			DirectoryEntry entry = new DirectoryEntry(ldap);
            DirectorySearcher mySearcher = new DirectorySearcher(entry);
			mySearcher.SecurityMasks = SecurityMasks.Dacl;
            List<string> searchProperties = new List<string>();
            if (filter != string.Empty)
            {
				//example LDAP filter
				//mySearcher.Filter = ("(&(objectCategory=computer)(!(userAccountControl:1.2.840.113556.1.4.803:=2)))");
				mySearcher.Filter = filter;
                if (props != string.Empty)
                {
                    searchProperties = props.Split(',').ToList();
                    foreach (string myKey in searchProperties)
                    {
                        mySearcher.PropertiesToLoad.Add(myKey);
                    }
                }
            }
            else
            {
                Console.WriteLine("[!] No LDAP search provided");
                Environment.Exit(0);
            }

            mySearcher.SizeLimit = int.MaxValue;
            mySearcher.PageSize = int.MaxValue;
            try
            {
                foreach (SearchResult mySearchResult in mySearcher.FindAll())
                {
                    // Get the 'DirectoryEntry' that corresponds to 'mySearchResult'.  
                    DirectoryEntry myDirectoryEntry = mySearchResult.GetDirectoryEntry();
                    // Get the properties of the 'mySearchResult'.  
                    ResultPropertyCollection myResultPropColl;
                    myResultPropColl = mySearchResult.Properties;

                    // return only specified attributes
                    if (searchProperties.Count > 0)
                    {
                        foreach (string attr in searchProperties)
                        {
                            // some attributes - such as memberof - have multiple values
                            for (int i = 0; i < mySearchResult.Properties[attr].Count; i++)
                            {
								Console.WriteLine(mySearchResult.Properties[attr][i].ToString());
                            }
                        }
                    }
                    // if no attributes specified, return all
                    else
                    {
                        foreach (string myKey in myResultPropColl.PropertyNames)
                        {
                            foreach (Object myCollection in myResultPropColl[myKey])
                            {
                                Console.WriteLine("{0} - {1}", myKey, myCollection);
                            }
                        }
                    }

                    mySearcher.Dispose();
                    entry.Dispose(); ;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[!] LDAP Search Error: {0}", ex.Message.Trim());
            }
        }
    }
}
