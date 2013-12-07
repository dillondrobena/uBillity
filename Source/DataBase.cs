/****************************************************************************************************************
 * Renato Bishop
 * 
 * 
 * DataBase class - Manages all I/O
 *                - Reads from and writes to text file
 *                - Keeps a database structure on the data storage file 
 *                      - Columns delimiter = tab space
 *                      - Row delimiter = new line
 *                      - Empty cells = "*"
 *                - At system initialization method, the data is stored into a Dictionary and returned               
 *                
 * Changes on development methodology: The database class handles all input and output 
 *                                    transactions during the programs life cycle. As
 *                                    suggested in the "Project Phase 3" assignment, we
 *                                    decided to use a text file to store all the data
 *                                    where as in the documentation of the program we 
 *                                    mentioned the developement of a SQL server. We 
 *                                    made this change for simplicity, since this is 
 *                                    just a software prototype.
 * 
 *****************************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.AccessControl;

namespace uBillity_Prototype
{   
    class DataBase
    {
        //Database empty constructor
        public DataBase() { }

        //Customer object to string
        private string custObj_toString(Customer cust) {
            string newCust = cust.getFirstName() + "\t" + cust.getLastName()
                + "\t" + cust.getSocialSecurity() + "\t" + cust.getContractType()
                + "\t" + cust.getContractLength() + "\t" + cust.getBillStreet()
                + "\t" + cust.getBillCity() + "\t" + cust.getBillState()
                + "\t" + cust.getBillZip() + "\t" + cust.getServiceStreet()
                + "\t" + cust.getServiceCity() + "\t" + cust.getServiceState()
                + "\t" + cust.getServiceZip() + "\t" + cust.getPhoneNumber()
                + "\t" + cust.getEmail() + "\t" + cust.getBillType()
                + "\t" + cust.getAutoBilling() + "\t" + cust.getBillRate()
                + "\t" + cust.getCurrentMeterReading();

            if (cust.getAutoBilling())                                      //Checks AutoBilling option, if true, credit card info is saved
            {
                newCust = newCust + "\t" + cust.getCardName()
                    + "\t" + cust.getCardNumber() + "\t" + cust.getCardExpiration()
                    + "\t" + cust.getCardSecurity() + "\t" + cust.getCardZip();
            }
            else {
                newCust = newCust + "\t*\t*\t*\t*\t*";
            }
            newCust = newCust + "\t" + cust.getPastMeterReading() + "\t" + cust.getMoneyOwed() + "\t" + cust.getAccountNumber() + "\t" + cust.getPreviousBalance();
            return newCust;
        }

        /*Adds new customer to database
         * Input: 
         * Customer object  
         *
         * Return: 
         * new Dictionary
         */
        public Dictionary<int, Customer> add_customer(Customer cust)
        {   
            add(cust);
            return getData();
        }
        
        //Adds customer to database
        private void add(Customer cust)
        {
            string newCust = custObj_toString(cust);                        //Customer object to string
            newCust = newCust + "\n";
            //get full path of the data file
            string pa = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string path = Path.Combine(pa, "uBillity\\Mock_Data.txt");
            File.AppendAllText(path, newCust);                              //store data to the end of text in file
        }

        /*Removes customer from database
         * Input: 
         * Customer object
         * 
         * Return: 
         * 0 = error, object not found
         * 1 = object deleted successfully 
         */
        public int remove_customer(Customer cust)
        {
            int status = remove(cust);
            return status;
        }

        //Removes Customer form database
        private int remove(Customer cust){
           
           //Gets text file full directory path 
           string pa = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
           string path = Path.Combine(pa, "uBillity\\Mock_Data.txt");
           System.IO.StreamReader file = new System.IO.StreamReader(path);
           int status = 0;
           string line = "";
           List<string> list = new List<string>();

           while ((line = file.ReadLine()) != null)                          //Verifies if customer existis in the data base
           {
               //skips all occurrencies of the customer in the Database and set status flag
               if (line.Contains(cust.getFirstName()) && line.Contains(cust.getLastName()))
               {
                   status = 1;
                   continue;
               }
               list.Add(line);
           }
           file.Close();
           if (status == 1)                                                 //if there is a change in the database
           {
               File.Delete(path);                                           //replace the old data with new data
               using (StreamWriter sw = File.CreateText(path))
               {
                   int c = 0;
                   while(c < list.Count() ){
                       sw.WriteLine(list[c]);
                       c++;
                   }
               }
           }
           return status;
        }


        //Retrieves data from file, builds Customer objects and stores into a dictionary
        public Dictionary<int, Customer> getData() 
        {
            Dictionary<int, Customer> dict = new Dictionary<int, Customer>();
            //Creates a file object 
            string pa = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string path = Path.Combine(pa, "uBillity\\Mock_Data.txt");
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            string line = "";
            int key = 1;
            string[] l;

            while ((line = file.ReadLine()) != null)                         //Extracts data from file into a data dictionary
            {
                l = line.Split('\t');                                       //extracts data from string and stores into and array
                int x = 0;
                while (x < 28)                                              //removing * from array
                {
                    if(l[x] == "*"){
                        l[x] = " ";
                    }
                    x++;
                }
                
                //rename the autobilling fields to comply with customer object method input specifications 
                if(l[16].CompareTo("True") == 0){
                    l[16] = "AUTO";
                }
                else
                {
                    l[16] = "MANUAL";
                }
                List<string> list = new List<string>(l);                    //store data from the array into a list
                Customer custObj = new Customer(list);                      //create and store customer into dictionary
                dict.Add(key, custObj);
                key++;
            }
            file.Close();
            return dict;
        }

        /*Initializes Database with existing customers
         * Input: 
         * NONE
         * 
         * Return: 
         * database dictionary
         */
        public Dictionary<int, Customer> initialize_DB()
        {
            Dictionary<int, Customer> dict = new Dictionary<int, Customer>();
            //Gets text file full directory path 
            string pa = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string path = Path.Combine(pa, "uBillity\\Mock_Data.txt");

            if (!File.Exists(path))                                           //Checks if data path exists
            {
                using (StreamWriter sw = File.CreateText(path))               // Create a file to write to. 
                {
                    sw.Write("");
                    return dict;
                }
            }
            else
            {
                StreamReader sr = new StreamReader(path);
                string str = sr.ReadLine();
                sr.Close();
                if (str != "")                                                //checks if there is any data stored in the text file
                {
                    return getData();
                }
                else
                {
                    return dict;
                }
            }
        }

        
    }
}