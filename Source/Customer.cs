/*
 * Geoffrey Giacoman
 * 11/26/13
 * CS 3354.001
 */

/******************** CUSTOMER CLASS ********************\
 *
 * Holds all necessary information for a customer. This includes billing and service address, meter reading,
 * bill rate, and credit card information (only if customer has auto billing). 
 *
 * Has setters and getters to change and return appropriate fields.
 * 
 * Calculates how much a bill is during a given month
 *
 */

/********************** DIFFERENCES *********************\
 *
 * No customer status, didn’t feel it was necessary for the prototype
 * No input validation. Felt that the prototype didn’t need it as we were just proving a concept.
 * If we were to implement a real version of the software, we would have included input validation.
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace uBillity_Prototype
{
    public class Customer
    {
        // Structure to hold billing address of customer
        private struct BillingAddress
        {
            public string billStreet;
            public string billCity;
            public string billState;
            public string billZip;
        }

        // Structure to hold service address of customer
        private struct ServiceAddress
        {
            public string serviceStreet;
            public string serviceCity;
            public string serviceState;
            public string serviceZip;
        }

        // Structure to hold credit card information of a customer
        private struct CreditCard
        {
            public string cardName;
            public string cardNumber;
            public string cardExpiration;
            public string cardSecurityCode;
            public string cardZip;
        }

        private string firstName;               // First name of customer
        private string lastName;                // Last name of customer
        private string socialSecurity;          // Social Security number of customer
        private string contractType;            // Holds contract type of customer (fixed or variable)
        private string contractLength;          // Holds contract length of customer
        private string phoneNumber;             // Phone number to reach customer
        private string email;                   // Email address to contact customer
        private string billType;                // paper/paperless
        private bool autoBilling;               // Determine if bill is paid manually or automatically
        private double billRate;                // Bill rate in KwH
        private double currentMeterReading;     // Current meter reading of customer
        private double pastMeterReading;        // Holds last months meter reading of a customer
        private double moneyOwed;               // How much a customer owes
        private string accountNumber;           // 9 digit account number of a customer
        private double previousBalance;
        private BillingAddress bill;            // Holds billing information of customer
        private ServiceAddress service;         // Holds service information of customer
        private CreditCard card;                // Holds credit card information of customer

        // Constructor to initialize fields of customer
        public Customer(List<string> list)
        {
            setFirstName(list[0]);                                          // Set first name
            setLastName(list[1]);                                           // Set last name
            setSocialSecurity(list[2]);                                     // Set social security number
            setContractType(list[3]); ;                                     // Set contract type
            setContractLength(list[4]);                                     // Set contract length
            setBill(list[5], list[6], list[7], list[8]);                    // Set up billing address
            setService(list[9], list[10], list[11], list[12]);              // Set up service address
            setPhoneNumber(list[13]);                                       // Set phone number
            setEmail(list[14]);                                             // Set email
            setBillType(list[15]);                                          // Set bill type
            setAutoBilling(list[16].ToUpper());                             // Determine if bill is paid manually or automatically
            setBillRate(list[17]);                                          // Set bill rate
            setCurrentMeterReading(list[18]);                               // Set current meter reading
            if (list.Count == 19 || list.Count == 24)                       // New Customer
            {
                setPastMeterReading(0);                                     // Initialize pastMeterReading to 0    
                setMoneyOwed(0);                                            // New customer has 0 balance 
                setAccountNumber();                                         // Set up customers account number
                setPreviousBalance(0);                                      // Set up previous balance
            }
            else if (list.Count == 23)                                       // Called when saving a customer   
            {
                setPastMeterReading(Convert.ToDouble(list[19]));
                setMoneyOwed(Convert.ToDouble(list[20]));
                setAccountNumber(list[21]);
                setPreviousBalance(Convert.ToDouble(list[22]));
            }
            else                                                             // Called when reading from database
            {
                setPastMeterReading(Convert.ToDouble(list[24]));
                setMoneyOwed(Convert.ToDouble(list[25]));
                setAccountNumber(list[26]);
                setPreviousBalance(Convert.ToDouble(list[27]));
            }

            if (autoBilling)                                                // If bill is paid automatically
                setCard(list[19], list[20], list[21], list[22], list[23]);  // Set up credit card information
        }// End Constructor
		//Set previous balance of bill
        public void setPreviousBalance(double balance)
        {
            previousBalance = balance;
        }
		//Get previous balance of the bill
        public double getPreviousBalance()
        {
            return previousBalance;
        }

        // Set the first name of a customer
        public void setFirstName(string name)
        {
            firstName = name;
        }// End setFirstName

        // Set the last name of a customer
        public void setLastName(string name)
        {
            lastName = name;
        }// End setLastName

        // Set social security number of a customer
        public void setSocialSecurity(string num)
        {
            socialSecurity = num;
        }// End setSocialSecurity

        // Set contract type of a customer
        public void setContractType(string type)
        {
            contractType = type;
        }// End setContractType

        // Set contract length of a customer
        public void setContractLength(string length)
        {
            contractLength = length;
        }// End setContractLength

        // Set billing street of a customer
        public void setBillStreet(string street)
        {
            bill.billStreet = street;
        }// End setBillStreet

        // Set billing city of a customer
        public void setBillCity(string city)
        {
            bill.billCity = city;
        }// End setBillCity

        // Set billing state of a customer
        public void setBillState(string state)
        {
            bill.billState = state;
        }// End setBillState

        // Set billing zip code of a customer
        public void setBillZip(string zip)
        {
            bill.billZip = zip;
        }// End setBillZip

        // Set billing information of a customer
        public void setBill(string street, string city, string state, string zip)
        {
            setBillStreet(street);
            setBillCity(city);
            setBillState(state);
            setBillZip(zip);
        }// End setBill

        // Set service street of a customer
        public void setServiceStreet(string street)
        {
            service.serviceStreet = street;
        }// End setServiceStreet

        // Set service city of a customer
        public void setServiceCity(string city)
        {
            service.serviceCity = city;
        }// End setServiceCity

        // Set service state of a customer
        public void setServiceState(string state)
        {
            service.serviceState = state;
        }// End setServiceState

        // Set service zip code of a customer
        public void setServiceZip(string zip)
        {
            service.serviceZip = zip;
        }// End setServiceZip

        // Set up service information of a customer
        public void setService(string street, string city, string state, string zip)
        {
            setServiceStreet(street);
            setServiceCity(city);
            setServiceState(state);
            setServiceZip(zip);
        }// End setService

        // Set phone number of a customer
        public void setPhoneNumber(string num)
        {
            phoneNumber = num;
        }// End setPhoneNumber

        // Set email of a customer
        public void setEmail(string mail)
        {
            email = mail;
        }// End setEmail
        
        // Set bill type of a customer
        public void setBillType(string type)
        {
            billType = type;
        }// End setBillType

        // Determine if customer pays bills automatically or manually
        public void setAutoBilling(string auto)
        {
            if (auto == "AUTO")         // Customer pays automatically
                autoBilling = true;
            else
                autoBilling = false;    // Customer pays manually
        }// End setAutoBilling

        // Set billing rate of a customer
        public void setBillRate(string rate)
        {
            billRate = Convert.ToDouble(rate);
        }// End setBillRate

        // Set current meter reading of a customer
        public void setCurrentMeterReading(string meter)
        {
            currentMeterReading = Convert.ToDouble(meter);
        }// End setCurrentMeterReading

        // Set past meter reading of a customer
        public void setPastMeterReading(double num)
        {
            pastMeterReading = num;
        }// End setPastMeterReading

        // Set how much a customer owes
        public void setMoneyOwed(double num)
        {
            moneyOwed += num;
        }// End setMoneyOwed

        // Set up account number of a customer
        public void setAccountNumber(string num)
        {
            accountNumber = num;
        }// End setAccountNumber

        public void setAccountNumber()
        {
            Random r = new Random();
            accountNumber = "";

            for (int i = 0; i < 10; i++)
            {
                int num = r.Next() % 10;    // Generate a random number between 0-9
                accountNumber += num;       // Add number to accountNumber
            }
        }

        // Set credit card name of a customer
        public void setCardName(string name)
        {
            card.cardName = name;
        }// End setCardName

        // Set credit card number of a customer
        public void setCardNumber(string num)
        {
            card.cardNumber = num;
        }// End setCardNumber

        // Set credit card expiration of a customer
        public void setCardExpiration(string exp)
        {
            card.cardExpiration = exp;
        }// End setCardExpiration

        // Set credit card security code of a customer
        public void setCardSecurityCode(string code)
        {
            card.cardSecurityCode = code;
        }// End setCardSecurityCode

        // Set credit card zip code of a customer
        public void setCardZip(string zip)
        {
            card.cardZip = zip;
        }// End setCardZip

        // Set up credit card information of a customer
        public void setCard(string name, string num, string exp, string code, string zip)
        {
            setCardName(name);
            setCardNumber(num);
            setCardExpiration(exp);
            setCardSecurityCode(code);
            setCardZip(zip);
        }// End setCard

        // Get first name of a customer
        public string getFirstName()
        {
            return firstName;
        }// End getFirstName

        // Get last name of a customer
        public string getLastName()
        {
            return lastName;
        }// End getLastName

        // Get full name of a customer
        public string getFullName()
        {
            return firstName + " " + lastName;
        }// End getFullName

        // Get social security number of a customer
        public string getSocialSecurity()
        {
            return socialSecurity;
        }// End getSocialSecurity

        // Get contractType of a customer
        public string getContractType()
        {
            return contractType;
        }// End getContractType

        // Get contract length of a customer
        public string getContractLength()
        {
            return contractLength;
        }// End getContractLength

        // Get billing street of a customer
        public string getBillStreet()
        {
            return bill.billStreet;
        }// End getBillStreet

        // Get billing city of a customer
        public string getBillCity()
        {
            return bill.billCity;
        }// End getBillCity

        // Get billing state of a customer
        public string getBillState()
        {
            return bill.billState;
        }// End getBillState

        // Get billing zip code of a customer
        public string getBillZip()
        {
            return bill.billZip;
        }// End getBillZip
        
        // Get full billing information of a customer
        public string getBillInfo()
        {
            return bill.billStreet + " " + bill.billCity + ", " + bill.billState + " " + bill.billZip;
        }// End getBillInfo

        // Get service street of a customer
        public string getServiceStreet()
        {
            return service.serviceStreet;
        }// End getServiceStreet

        // Get service city of a customer
        public string getServiceCity()
        {
            return service.serviceCity;
        }// End getServiceCity

        // Get service state of a customer
        public string getServiceState()
        {
            return service.serviceState;
        }// End getServiceState

        // Get service zip code of a customer
        public string getServiceZip()
        {
            return service.serviceZip;
        }// End getServiceZip

        // Get full service information of a customer
        public string getServiceInfo()
        {
            return service.serviceStreet + " " + service.serviceCity + ", " + service.serviceState + " " + service.serviceZip;
        }// End getServiceInfo

        // Get phone number of a customer
        public string getPhoneNumber()
        {
            return phoneNumber;
        }// End getPhoneNumber

        // Get email address of a customer
        public string getEmail()
        {
            return email;
        }// End getEmail

        // Get billing type of a customer
        public string getBillType()
        {
            return billType;
        }// End getBillType

        // Get customers payment method (automatically or manually)
        public bool getAutoBilling()
        {
            return autoBilling;
        }// End getAutoBilling

        // Get billing rate of a customer
        public double getBillRate()
        {
            return billRate;
        }// End getBillRate

        // Get the current meter reading of a customer
        public double getCurrentMeterReading()
        {
            return currentMeterReading;
        }// End getCurrentMeterReading

        // Get last months meter reading of a customer
        public double getPastMeterReading()
        {
            return pastMeterReading;
        }// End getPastMeterReading

        // Get how much money a customer owes
        public double getMoneyOwed()
        {
            return moneyOwed;
        }// end getMoneyOwed

        // Get account number of a customer
        public string getAccountNumber()
        {
            return accountNumber;
        }// End getAccountNumber

        // Get credit card name of a customer
        public string getCardName()
        {
            return card.cardName;
        }// End getCardName

        // Get credit card number of a customer
        public string getCardNumber()
        {
            return card.cardNumber;
        }// End getCardNumber

        // Get credit card expiration date of a customer
        public string getCardExpiration()
        {
            return card.cardExpiration;
        }// End getCardExpiration

        // Get credit card security code of a customer
        public string getCardSecurity()
        {
            return card.cardSecurityCode;
        }// End getCardSecurity

        // Get credit card zip code of a customer
        public string getCardZip()
        {
            return card.cardZip;
        }// End getCardZip

        // Get full credit card information of a customer
        public string getCardInfo()
        {
            return card.cardName + " " + card.cardNumber + " " + card.cardExpiration + " " + card.cardSecurityCode + " " + card.cardZip;
        }// End getCardInfo

        // Update the meter reading of a customer
        public void updateMeterReading(double num)
        {
            if (num > getCurrentMeterReading())
            {
                setPastMeterReading(getCurrentMeterReading());
                setCurrentMeterReading(num.ToString());
            }
        }// End updateMeterReading

        public double getPreviousBillAmount()
        {
            double amount = getPastMeterReading();   // Get monthly KwH usage

            if (amount > 0)
            {
                if (getContractType().ToUpper() == "COMMERCIAL")// Customers contract is commercial
                {
                    amount *= getBillRate();                    // Get total amount of bill

                }
                else                                            // Customers contract is residential, rate goes down 10% per 100 KwH
                {
                    double num = 0;
                    double rate = getBillRate();                // Get bill rate

                    while (amount > 0.0)
                    {
                        if (amount >= 100)                      // More than 100 KwH usage
                        {
                            num += (100 * rate);
                            rate = rate - (rate * 0.10);        // Update rate to be used to calculate bill
                            amount -= 100;
                        }
                        else                                    // Less than 100 KwH, calculate rest of amount to be added to bill
                        {
                            num += (amount * rate);
                            amount -= (amount + 1);
                        }
                    }

                    amount = num;
                }
            }

            return amount;
        }

        // Determine the monthly bill cost
        public double getBillAmount(double meterReading)
        {
            double amount = getCurrentMeterReading();  // Get monthly KwH usage

            if (amount > 0)
            {
                if (getContractType().ToUpper() == "COMMERCIAL")// Customers contract is commercial
                {
                    amount *= getBillRate();                    // Get total amount of bill

                }
                else                                            // Customers contract is residential, rate goes down 10% per 100 KwH
                {
                    double num = 0;
                    double rate = getBillRate();                // Get bill rate

                    while (amount > 0.0)
                    {
                        if (amount >= 100)                      // More than 100 KwH usage
                        {
                            num += (100 * rate);
                            rate = rate - (rate * 0.10);        // Update rate to be used to calculate bill
                            amount -= 100;
                        }
                        else                                    // Less than 100 KwH, calculate rest of amount to be added to bill
                        {
                            num += (amount * rate);
                            amount -= (amount + 1);
                        }
                    }

                        amount = num;
                }
            }

            setMoneyOwed(amount);                               // Set how much a customer owes

            return amount;
        }// End getBillAmount

        // Customer paying bill
        public void makePayment(double num)
        {
            if (num > getMoneyOwed())
                setMoneyOwed(getMoneyOwed() * -1);
            else if (num > 0)
                setMoneyOwed(num * -1);
        }// End makePayment
    }// End Customer
}
