/*
 * Dylan Williams
 * 11/28/2013
 * CS 3354.001
 * 
 * Main class:
 *      -Starting point for the application
 *      -Creates the UI
 *      -Maintains the local database of customers within the program
 *      -Makes sure the customer information in the program is consistent with what is in the text file
 *      
 * Differences from Design Document:
 *      -The main class maintains a dictionary of customers that are in the database and ensures agreement between this and the text database.
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace uBillity_Prototype
{
    static class uBillity
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static Dictionary<int, Customer> custArray;
        static int key;
        static DataBase dataBase;

        [STAThread]
        static void Main()
        {
            dataBase = new DataBase();                              //create the database
            custArray = dataBase.initialize_DB();                   //get the initial database from the file, store in a dictionary
            key = custArray.Count + 1;                              //keep track of keys in the customer dictionary to insure no duplicate keys or overwrites
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new uBillity_UI());                     //initialize the UI
        }

        internal static void addCustomer(List<string> newCustList)
        {
            //add customer to the custArray
            Customer newCust = new Customer(newCustList);
            custArray.Add(key, newCust);
            key++;

            //add customer to the file
            dataBase.add_customer(newCust);
        }

        internal static void deleteCustomer(string socialSecurityNum)
        {
            //find the customer in the custArray first
            foreach (KeyValuePair<int, Customer> entry in custArray)
            {
                if (socialSecurityNum.Equals(entry.Value.getSocialSecurity(), StringComparison.Ordinal))
                {
                    //remove from file
                    dataBase.remove_customer(entry.Value);

                    //remove from custArray
                    custArray.Remove(entry.Key);
                    break;
                }
            }
        }

        internal static void updateCustomer(Customer customer)
        {   //must remove the original customer from the file first, then add the customer with new information
            dataBase.remove_customer(customer);
            dataBase.add_customer(customer);
        }

        internal static void updateCustomer(List<string> updateCustList)
        {
            foreach (KeyValuePair<int, Customer> entry in custArray)
            {
                if (updateCustList[2].Equals(entry.Value.getSocialSecurity(), StringComparison.Ordinal))
                {
                    //remove from file
                    dataBase.remove_customer(entry.Value);

                    //remove from custArray
                    custArray.Remove(entry.Key);

                    //add to file and custArray
                    addCustomer(updateCustList);
                    break;
                }
            }
        }

        internal static Dictionary<int, Customer> getCustomers()
        {
            return custArray;
        }

        internal static Customer findCustByName(string firstName, string lastName)
        {
            string currentFirstName;
            string currentLastName;

            foreach (KeyValuePair<int, Customer> entry in custArray)
            {
                currentFirstName = entry.Value.getFirstName();
                currentLastName = entry.Value.getLastName();

                if (firstName.Equals(currentFirstName, StringComparison.OrdinalIgnoreCase) && lastName.Equals(currentLastName, StringComparison.OrdinalIgnoreCase))
                {
                    return entry.Value;     //if the customer is found, return that customer object
                }
            }
            return null;                    //if not, return a null pointer
        }

        internal static Customer findCustBySSN(string socialSecurityNum)
        {
            foreach (KeyValuePair<int, Customer> entry in custArray)
            {
                if(socialSecurityNum.Equals(entry.Value.getSocialSecurity(), StringComparison.Ordinal))
                {
                    return entry.Value;     //if the customer is found, return that customer object
                }
            }
            return null;                    //if not, return a null pointer
        }


    }
}
