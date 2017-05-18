using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DataLinkedList
{
    class Program
    {
        public class LinkedList
        {
            Node head;
            Node current;

            int idCounter = 1;
            public void Add(Node newNode)
            {
                newNode.Address.AddressId = idCounter++;
                if (head == null)
                {
                    current = head = newNode;
                }
                else
                {
                    current.next = newNode;
                    current = current.next;
                }
            }

            public void Print()
            {
                current = head;
                while (current != null)
                {
                    var x = current.Address;
                    Console.WriteLine(x.AddressId + "\n" + x.FirstName + " " + x.LastName + "\n" + x.Street + x.City + ", " + x.State + " " + x.Zip + "\n");
                    current = current.next;
                }
            }

            public void Delete(int id)
            {
                Console.WriteLine("You are about to delete address id: " + id);
                Console.ReadLine();
                current = head;
                Node previous = new Node();

                while (current != null)
                {
                    if (current.Address.AddressId == id)
                    {
                        previous.next = current.next;
                        current = current.next;
                    }
                    else
                    {
                        previous = current;
                        current = current.next;
                    }
                }
                Print();
                Console.ReadLine();

            }
        }

        public class Node
        {
            public Node next;
            public Address Address;
        }

        public static void Main(string[] args)
        {
            LinkedList deliveryRoutes = new LinkedList();
            using (var csvReader = File.OpenRead(@"C:\Users\Cristian\codecamp\m2-c#\DataLinkedList\DataLinkedList\Data\addressList.csv"))
            using (var routes = new StreamReader(csvReader))
            {
                while (!routes.EndOfStream)
                {
                    var line = routes.ReadLine();
                    var values = line.Split(';');
                    string[] addressArray = values[0].Split(',');

                    Address newAddress = new Address
                    {
                        FirstName = addressArray[0],
                        LastName = addressArray[1],
                        Street = addressArray[2],
                        City = addressArray[3],
                        State = addressArray[4],
                        Zip = addressArray[5]
                    };

                    Node newNode = new Node();
                    newNode.Address = newAddress;

                    deliveryRoutes.Add(newNode);
                }

                deliveryRoutes.Print();
                Console.WriteLine("\n Which address do you want to delete ?");
                string idInput = Console.ReadLine();
                deliveryRoutes.Delete(Int32.Parse(idInput));
            }
        }

    }
}
