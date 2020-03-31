using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cis237_assignment5
{
    class BeverageRepository : UserInterface
    {
        // Make new instance of the BeverageContext
        BeverageContext _beverageContext = new BeverageContext();

        // Private Variables
        private Beverage[] beverages;
        private int beverageLength;

        // Constructor. Must pass the size of the collection.
        public BeverageRepository(int size)
        {
            this.beverages = new Beverage[size];
            this.beverageLength = 0;
        }

        // Add a new item to the collection
        public void AddNewBeverage(
            string id,
            string name,
            string pack,
            decimal price,
            bool active,
            Beverage newBeverageToAdd
        )
        {
            // Use a try catch to ensure that they can't add a car with an id that already exists
            try
            {
                // Add the new beverage to the Beverage Collection
                _beverageContext.Beverages.Add(newBeverageToAdd);

                // Saving the changes to the database
                _beverageContext.SaveChanges();

                DisplayAddBeverageItemSuccess();
            }
            catch (Exception e) // catch (DbUpdateException e)
            {
                // Remove the new beverage form the Beverages Collection since we can't save it.
                // Dont want it to hang around the next time to saveChanges
                _beverageContext.Beverages.Remove(newBeverageToAdd);
                // Error message
                DisplayBeverageAlreadyExistsError();
                //userInterface.DisplayItemAlreadyExistsError();
            }
            //catch (Exception e)
            //{
            //    // Remove the new beverage form the Beverages Collection since we can't save it.
            //    // Dont want it to hang around the next time to saveChanges
            //    _beverageContext.Beverages.Remove(newBeverageToAdd);
            //    // Error message
            //    DisplayItemAlreadyExistsError2();
            //    //userInterface.DisplayItemAlreadyExistsError2();
            //}

            // Fetch out the record we just added and ensure that it exists
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Just added a new beverage. Going to fetch it and print to verify");
            Console.WriteLine();
        }

        // ToString override method to convert the collection to a string
        public override string ToString()
        {
            // Declare a return string
            string returnString = "";

            // Loop through all of the beverages
            foreach (Beverage beverage in beverages)
            {
                // If the current beverage is not null, concat it to the return string
                if (beverage != null)
                {
                    returnString += beverage.ToString() + Environment.NewLine;
                }
            }
            // Return the return string
            return returnString;
        }

        //Print Beverage's
        public void PrintList()
        {
            Console.WriteLine("Print the list of beverages");
            foreach (Beverage beverage in _beverageContext.Beverages)
            {
                Console.WriteLine(BeverageToString(beverage));
            }
        }


        // Find an item by it's Id
        public string FindById(string id)
        {
            // Declare return string for the possible found item
            string returnString = null;


            /* Call the where method on the Cars table and pass in a lambda expression
               for the criteria we are looking for. There is nothing special about the word
               car in the part that read: car => car.id == "V0...". It could be
               any characters we want it to be.
               It is just a variable name for the current car we are considering
               in the expression. This will automatically loop through all the cars,
               and run the expression against each of them. WHen the result is finally
               true, it will return that car */
            Beverage _beverageToFind = _beverageContext.Beverages.Where(beverage => beverage.id == id).First();

            returnString = BeverageToString(_beverageToFind);

            return returnString;
        }

        public void UpdateBeverage(string value)
        {
            // Find Beverage to update
            Beverage beverageToUpdate = _beverageContext.Beverages.Find(value);

            // Output hte beverage to update
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("About to do an update on this beverage");
            Console.WriteLine(BeverageToString(beverageToUpdate));
            Console.WriteLine("Doing update now");

            // Update some of the properties of the car we found.
            // Don't need to update all of them if we do not want to.

            beverageToUpdate.name = GetNewNameInformation();
            beverageToUpdate.pack = GetNewPackInformation();
            beverageToUpdate.price = GetNewPriceInformation();
            beverageToUpdate.active = GetNewActiveInformation();

            // Save the changes to the database. Since when we pulled out the one to update,
            // all we really doing was getting a reference to the one in the collection
            // that we wanted to update, there is no need to 'put' the car back into the Cars
            // Collection. It is still there. All we have to do is save the changes
            _beverageContext.SaveChanges();

            // Get the beverage back out of the database
            beverageToUpdate = _beverageContext.Beverages.Find(value);

            // Output the beverage to verify it got updated
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Outputting the updated beverage to make sure update worked");
            Console.WriteLine(BeverageToString(beverageToUpdate));
        }

        public void DeleteBeverage(string deletedValue)
        {
            // Get a beverage out of the database that we want to delete
            Beverage beverageToDelete = _beverageContext.Beverages.Find(deletedValue);

            // Remove the beverage from the Beverages Collection
            _beverageContext.Beverages.Remove(beverageToDelete);

            // Save the changes to the database
            _beverageContext.SaveChanges();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Deleted the added beverage. Looking to see if it is still in the database");

            // Try to get the beverage out of the database and print it
            beverageToDelete = _beverageContext.Beverages.Find(deletedValue);
            if (beverageToDelete == null)
            {
                Console.WriteLine("The beverage you're looking for does not exist");
            }
            else
            {
                Console.WriteLine("Beverage is still in the database");
            }
        }

        static string BeverageToString(Beverage beverage)
        {
            return $"{beverage.id} {beverage.name} {beverage.pack} {beverage.price} {beverage.active}";
        }
    }
}
