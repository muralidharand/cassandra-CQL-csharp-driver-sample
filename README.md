Cassandra CQL c-sharp driver sample
==================================

Prerequisites
-------------
* Cassandra 1.2.5
 * Download [Datastax - Cassandra](http://planetcassandra.org/Download/DataStaxCommunityEdition)
* Cassandra Ado.Net driver for CQL 3 (Cassandra.dll & Cassandra.data.dll).
 * Download [Cassandra driver](https://github.com/datastax/csharp-driver)
* .Net framework version 
 * 4.0 or higher
* IDE
 * Visual Studio 2010 or higher
 * SharpDevelop 4.2.2 or higher


Installing
----------
This Cassandra CQL driver has not been released yet and will need to be compiled manually. See README.rst from 
the [Datastax csharp driver project](https://github.com/datastax/csharp-driver/blob/master/Cassandra.Data/README.rst)
 * Open Machine.config from c:\WINDOWS\Microsoft.NET\Framework\v4.0.30319\Config\.
 * Go To --> `<system.data> <DbProviderFactories>` section
 * Add the following
````
    <add name="Cassandra Data Provider" invariant="Cassandra.Data.CqlProviderFactory"
         description=".Net Framework Data Provider for Cassandra"
         type="Cassandra.Data.CqlProviderFactory, Cassandra.Data"  />
````         
 * Register Cassandra.dll & Cassandra.data.dll in your GAC.
 * If you're not able do register with GAC, then add Cassandra.dll & Cassandra.data.dll assemblies in your project.    
  
Getting started with sample appication
======================================
 * Build the application
 * Press F5
 * Press Start button the Cassandra CQL Basics form.
  * Connect your local cassandra instance on 9042 number.  Cassandra connectionstring - "Contact Points=localhost;Port=9042";
  * Create a new keyspace called "mykeyspace".
  * Use the newly created keyspace "mykeyspace".
  * Create a new table called "Student" with few fields.
  * Insert 10 sample records into the "Student" table.
  * Read all the records from "Student" table.
  * Update first record with age - 16.
  * Delete a record from the "Student" table.
  * Drop the "Student" table.
  * Drop the "mykeyspace" keyspace.
    
    
 
 







