/*
 * Created by SharpDevelop.
 * User: muralidharand
 * Date: 6/21/2013
 * Time: 11:17 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Cassandra_CQL_Basics
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void Button1_Click(object sender, EventArgs e)
		{
			var provider = DbProviderFactories.GetFactory("Cassandra.Data.CqlProviderFactory");
            var connection = provider.CreateConnection();
            connection.ConnectionString = "Contact Points=localhost;Port=9042";
            connection.Open();
            
            var command = connection.CreateCommand();
            string keyspaceName  = "mykeyspace";
            
            //create a new keyspace here
            command.CommandText = string.Format(@"CREATE KEYSPACE {0} 
                     WITH replication = {{ 'class' : 'SimpleStrategy', 'replication_factor' : 1 }};"
                 , keyspaceName);
            command.ExecuteNonQuery();
            
            //connect to the connected keyspace
            connection.ChangeDatabase(keyspaceName);
            
            
            //create a new table here
            
            string tableName = "Student";
            command.CommandText = string.Format(@"CREATE TABLE {0}(
                                  StudentId uuid,
                                  Name text,
                                  FirstName text,
                                  LastName text,
                                  Age int,
                                  Gender boolean,
                                  PRIMARY KEY(StudentId))", tableName);
            command.ExecuteNonQuery();
            
            
            //Insert few records into new table
            StringBuilder insertStatement = null;
            for (int i = 0; i < 10; i++) {
				insertStatement = new StringBuilder();
				insertStatement.AppendFormat(@"INSERT INTO {0} (
				                    StudentId,
				                    Name,
				                    FirstName,
				                    LastName,
				                    Age,
				                    Gender)
				                    VALUES ({1},'{2}','{3}','{4}',{5},{6});", 
				                         tableName, Guid.NewGuid().ToString(), "Student -"+ i.ToString(),"Firstname - " + i.ToString(),"Lastname -" + i.ToString(),new Random().Next(10,15),"true");
				
				
				command.CommandText = insertStatement.ToString();
				command.ExecuteNonQuery();   
            }
            
            
            //Read the records from the table
            command = connection.CreateCommand();
        	command.CommandText = "select * from " + tableName;        	
        	
			//If the table is very simple then use DataSet to fetch the records.
        	DataSet ds = new DataSet();
        	DbDataAdapter adap = new Cassandra.Data.CqlDataAdapter();
        	adap.SelectCommand = command;
        	adap.Fill(ds);
        	MessageBox.Show(ds.Tables[0].Rows.Count.ToString());
        	
        	// If the table is too large then use DataReader instead of DataSet.
        	/*
        	DbDataReader reader = command.ExecuteReader();        	
        	if (reader.HasRows)
        	{
        		int i = 0;
        		while (reader.NextResult())
        		{
        			i = i+1;        			
        		}
        		MessageBox.Show(i.ToString());
        	}        	
        	*/
        	
        	//Update existing record
			//The below studentId is used to update the existing record.
			Guid forUpdateStudentId =(Guid)  ds.Tables[0].Rows[0][0];
        	command = connection.CreateCommand();
        	command.CommandText = "update " + tableName + " set age = 16 where StudentId = " + forUpdateStudentId;     
        	command.ExecuteNonQuery();
     
        	//Delele a record
			//The below studentId is used to delete a record.
			Guid forDeleteStudentId =(Guid) ds.Tables[0].Rows[ new Random().Next(0,ds.Tables[0].Rows.Count)][0];
        	command = connection.CreateCommand();
        	command.CommandText = "delete from " + tableName +" where StudentId = " + forDeleteStudentId;     
        	command.ExecuteNonQuery();             	
        	
        	//drop Student table
        	command = connection.CreateCommand();
        	command.CommandText = "drop table " + tableName;     
        	command.ExecuteNonQuery();
        	
        	//drop keyspace
        	command = connection.CreateCommand();
        	command.CommandText = "drop keyspace " + keyspaceName;     
        	command.ExecuteNonQuery();
        	
        	MessageBox.Show("All done !");
            			
		}
	}
}
