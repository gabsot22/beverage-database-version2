/* Gabe Soto
 * CIS 237 MW 6:00-8:15pm
 * 4/1/20
 **/
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

        // Add a new item to the database
        public void AddNewBeverage(
            string id,
            string name,
            string pack,
            decimal price,
            bool active,
            Beverage newBeverageToAdd
        )
        {
            // Use a try catch to ensure that they can't add a beverage with an id that already exists
            try
            {
                // Add the new beverage to the Beverage Collection
                _beverageContext.Beverages.Add(newBeverageToAdd);

                // Saving the changes to the database
                _beverageContext.SaveChanges();

                // Display Succession
                DisplayAddBeverageItemSuccess();
            }
            catch (Exception e) // catch (DbUpdateException e)
            {
                // Remove the new beverage form the Beverages Collection since we can't save it.
                // Dont want it to hang around the next time to saveChanges
                _beverageContext.Beverages.Remove(newBeverageToAdd);

                // Error message
                DisplayBeverageAlreadyExistsError();
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
        }

        //Print Beverage's
        public void PrintList()
        {
            // Loop through each beverage and print out
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

            /* Call the where method on the Beverages table and pass in a lambda expression
               for the criteria we are looking for. There is nothing special about the word
               beverage in the part that read: beverage => beverage.id == id. It could be
               any characters we want it to be.
               It is just a variable name for the current car we are considering
               in the expression. This will automatically loop through all the beverages,
               and run the expression against each of them. WHen the result is finally
               true, it will return that beverage */
            try
            {
                Beverage _beverageToFind = _beverageContext.Beverages.Where(beverage => beverage.id == id).First();

                // Set the found beverage to the return string so it doesn't return null
                returnString = BeverageToString(_beverageToFind);
            }
            catch (Exception e)
            {
                DisplayItemFoundError();
            }
            return returnString;
        }

        public void UpdateBeverage(string value)
        {

            // Find Beverage to update
            Beverage beverageToUpdate = _beverageContext.Beverages.Find(value);

            try
            {
                // Output the beverage to update
                DisplayUpdateBeverageHeading();

                // Output Beverage we are going to update
                Console.WriteLine(BeverageToString(beverageToUpdate));

                // Update some of the properties of the beverage we found.
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

                DisplayFinalHeadingAddBeverage();

                // Output the beverage to verify it got updated
                Console.WriteLine(BeverageToString(beverageToUpdate));
            }
            catch (Exception e)
            {
                DisplayDeleteBeverageError();
            }
        }

        // DELETING
        public void DeleteBeverage(string deletedValue)
        {
            // Get a beverage out of the database that we want to delete
            Beverage beverageToDelete = _beverageContext.Beverages.Find(deletedValue);

            try
            {
                // Remove the beverage from the Beverages Collection
                _beverageContext.Beverages.Remove(beverageToDelete);

                // Save the changes to the database
                _beverageContext.SaveChanges();

                // Display heading
                DisplayDeleteBeverageHeading1();

                // Try to get the beverage out of the database and print it
                beverageToDelete = _beverageContext.Beverages.Find(deletedValue);
                if (beverageToDelete == null)
                {
                    // User interface error heading
                    DisplayDeleteBeverageError();
                    DisplayDeleteBeverageSuccession();
                }
                else
                {
                    // User interface found heading
                    DisplayBeverageStillInDataBase();
                }
            }
            catch (Exception e)
            {
                DisplayDeleteBeverageError();
            }
        }

        // Beverage output string
        public static string BeverageToString(Beverage beverage)
        {
            return $"{beverage.id} {beverage.name} {beverage.pack} {beverage.price} {beverage.active}";
        }
    }
}
