using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace EmployeeDirectoryManagerApplication
{
    public sealed class EmployeeManager
    {
        public BindingList<Employee> Employees { get; } = new BindingList<Employee>();

        public void AddEmployee(Employee e)
        {
            if (e == null)
                { throw new ArgumentNullException(nameof(e)); }

            else if (string.IsNullOrWhiteSpace(e.EmployeeID)) //
                { throw new ArgumentException("Employee ID is required.", nameof(e.EmployeeID)); }

            else if (string.IsNullOrWhiteSpace(e.FullName)) //checks for an empty textbox
                { throw new ArgumentException("Full name is required.", nameof(e.FullName)); }
            
            else if (string.IsNullOrWhiteSpace(e.Department)) //checks for an empty textbox
            { throw new ArgumentException("Department is required.", nameof(e.Department)); }

            else if (string.IsNullOrWhiteSpace(e.Role)) //checks for an empty textbox
            { throw new ArgumentException("Role is required.", nameof(e.Role)); }

            else if (string.IsNullOrWhiteSpace(e.Salary.ToString()))//checks for an empty textbox
            { throw new ArgumentException("Salary is required.", nameof(e.Salary)); }

        }
        

        public bool DeleteEmployee(string employeeID, string fullName)
        {
            // Find the exact booking (case-insensitive comparison)
            var toRemove = Employees.FirstOrDefault(b =>
                b.EmployeeID.Equals(employeeID, StringComparison.OrdinalIgnoreCase) &&
                b.FullName.Equals(fullName, StringComparison.OrdinalIgnoreCase));

            if (toRemove == null)
            {
                return false;// not found
            }
            else
            {
                Employees.Remove(toRemove);
                return true;// successfully deleted employee
            }
        }

        public bool TryToFindEmployee(string employeeID, string fullName, out Employee employee)
        {
            employee = Employees.FirstOrDefault(b =>
            b.EmployeeID.Equals(employeeID, StringComparison.OrdinalIgnoreCase) &&
            b.FullName.Equals(fullName, StringComparison.OrdinalIgnoreCase));

            return employee != null;
        }
        /*Created a public bool IsAvailable() passes in roomNumber and DateTime
        for checkIn and checkOut
        //Used a try catch block run EnsureNoOverlap. Returns true if successful,
        catch and returns false if not*/

        public bool IsAvailable(string roomNumber, DateTime checkIn, DateTime checkOut)
        {
            try
            {
                EnsureNoOverlap(roomNumber, checkIn, checkOut, except: null);
                return true;   // no exception = available
            }
            catch (InvalidOperationException)
            {
                return false;  // overlap found
            }
        }

        //method to reschedule booking
        public void RescheduleBooking(string roomNumber, string guestName, DateTime newCheckIn, DateTime newCheckOut)
        {
            // Find the booking to reschedule
            if (!TryFindBooking(roomNumber, guestName, out Booking? booking) || booking == null)
            {
                throw new InvalidOperationException($"No booking found for guest '{guestName}' in room '{roomNumber}'.");
            }

            // Check for overlap with OTHER bookings (exclude this one)
            if (IsAvailable(roomNumber, newCheckIn, newCheckOut))
            {
                // If no overlap → apply the change
                booking.Reschedule(newCheckIn, newCheckOut);
            }
            else
            {
                throw new InvalidOperationException(
                    $"Room {roomNumber} already booked {newCheckIn:MM/dd}–{newCheckOut:MM/dd}.");
            }


        }


        //!!! Helper method for you to check if a room has an overlapping
        //visit.Do not modify
        private void EnsureNoOverlap(string roomNumber, DateTime checkIn, DateTime checkOut, Booking? except)
        {
            bool Overlaps(Booking a) => a.CheckIn < checkOut && checkIn <
            a.CheckOut;
            foreach (var existing in _bookings)
            {
                if (except != null && ReferenceEquals(existing, except)) continue;
                if (!existing.RoomNumber.Equals(roomNumber,
                StringComparison.OrdinalIgnoreCase)) continue;

                if (Overlaps(existing))
                {
                    throw new InvalidOperationException(
                    $"Room {roomNumber} already booked {existing.CheckIn:MM/dd}–{existing.CheckOut:MM/dd}.");
                }
            }
        }

        //!!! Helper Methods for the CSV. Do not modify anything below this
        //line
// -------- Persistence (CSV) --------
public void SaveToCsv(string path)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(path))
            ?? ".");
            var sw = new StreamWriter(path, false, new
            UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
            sw.WriteLine("Id,FullName,Department,Role,Salary,HireDate");
            foreach (var e in Employees)
                sw.WriteLine(e.ToCsv());
        }
        public void LoadFromCsv(string path)
        {
            var sr = new StreamReader(path, Encoding.UTF8);
            Employees.Clear();
            string header = sr.ReadLine(); // skip header
            if (header is null) throw new InvalidDataException("File is empty.");
            int line = 1, loaded = 0, skipped = 0;
            while (!sr.EndOfStream)
            {
                string row = sr.ReadLine();
                line++;
                if (string.IsNullOrWhiteSpace(row)) continue;
                try
                {
                    var e = Employee.FromCsv(row);
                    // Enforce unique ID on load as well
                    if (Employees.Any(x => string.Equals(x.EmployeeID, e.EmployeeID, StringComparison.OrdinalIgnoreCase)))
                        throw new InvalidOperationException($"Duplicate ID '{e.EmployeeID}'in file.");
                        Employees.Add(e);
                    loaded++;
                }
                catch (Exception ex)
                {
                    skipped++;
                    // For classroom apps, a console note is fine; in prod you'd
                    //log this.Console.WriteLine($"Skipped line {line}: {ex.Message}");
                }
            }
            Console.WriteLine($"Load complete. Loaded: {loaded}, Skipped:{skipped}");
        }
    }
}

    }
}
