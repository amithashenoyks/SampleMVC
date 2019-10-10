using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Text;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Queue;

namespace MVC_Tutorial.AzureAccess
{
    public class Storage
    {
        public void DownloadBlob()
        {
            // ARRANGE
            string filename = "dragonfly.jpg";
            string containerName = "images";
            StorageCredentials creds = new StorageCredentials("storageaccountamita", "Dvgs09YaZM+RjOPsehrg7kkQgidRrL2Q9lez22jqvser+0QP/aOmUTtUMAOxfJ874n+Vk+I3sxoidCLCaZZGbg==");
            CloudStorageAccount acct = new CloudStorageAccount(creds, true);
            CloudBlobClient client = acct.CreateCloudBlobClient();
            CloudTableClient tblClient = acct.CreateCloudTableClient();
            CloudQueueClient qClient = acct.CreateCloudQueueClient();


            CloudBlobContainer container = client.GetContainerReference(containerName);
            container.CreateIfNotExists();

            //BlobContainerPermissions permissions = new BlobContainerPermissions();
            //permissions.PublicAccess = BlobContainerPublicAccessType.Container;
            //container.SetPermissions(permissions, null, null, null);
            ICloudBlob blob = container.GetBlockBlobReference(filename);

            //  using (MemoryStream fileStream = new MemoryStream(Encoding.UTF8.GetBytes(@"E:\2019\MVC_Tutorial\Images\pup.jpg")))
            using (FileStream fileStream = System.IO.File.OpenRead(@"E:\2019\MVC_Tutorial\Images\pup.jpg"))
            {
                // blob.UploadFromStream(fileStream);
                blob.DownloadToStream(fileStream);
                blob.DeleteIfExists();
            }
            // ASSERT
            //   Assert.That(blob.Properties.Length, Is.EqualTo(fileContents.Length));
        }

        public void UploadTableFromConfig()
        {
            // ARRANGE
            StorageCredentials creds = new StorageCredentials("storageaccountamita", "Dvgs09YaZM+RjOPsehrg7kkQgidRrL2Q9lez22jqvser+0QP/aOmUTtUMAOxfJ874n+Vk+I3sxoidCLCaZZGbg==");

            CloudStorageAccount acct =new  CloudStorageAccount(creds, true);
            CloudTableClient client = acct.CreateCloudTableClient();
            var table = client.GetTableReference("Article");
            Details entity = new Details("1","asdf");
            entity.ID = 1;
            entity.Title = "azure";
            entity.Description = "new in azure";
            entity.Price = 100;

                
            // ACT
            table.CreateIfNotExists(); // create table
           TableOperation insert = TableOperation.Insert(entity);
           table.Execute(insert); // insert record
           // TableOperation fetch = TableOperation.Retrieve<Article>("1", "asdf");
          //  TableResult result = table.Execute(fetch); // fetch record
          //  TableOperation del = TableOperation.Delete(result.Result as Record);
            //table.Execute(del); // delete record


        }
    }

    public class Details :TableEntity
    {
        public Details() : this(DateTime.UtcNow.ToShortDateString(), Guid.NewGuid().ToString())
        { }
        public Details(string partitionKey, string rowKey)
        {
            this.PartitionKey = partitionKey;
            this.RowKey = rowKey;
        }

        public int ID { get; set; }
       
        public string Title { get; set; }

       
        public string Description { get; set; }

    
        public int Price { get; set; }
    }





}