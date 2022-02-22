using System;
using System.Collections;
using MySql.Data.MySqlClient;

namespace Projet_1_Shell {
    class Program {
        string[,] tableau = new string[10, 7];
        private ArrayList listArray = new ArrayList();

        // Variable pour garder le # total de pensionnaires
        int totalAnimal = 0;
        // Variable pour chaque nouveau ID à utiliser
        int nextID = 1;

        static void Main(string[] args) {
            Program program = new Program();
            program.startTheMachine();
        }

        private void activityLoop() {
            do {
                afficherMenu();
            } while (true);
        }

        private void checkList() {
            MySqlConnection connex2 = connectToDatabase();
            string sqlString = "SELECT id FROM Animals";
            MySqlCommand command = new MySqlCommand(sqlString, connex2);

            using (MySqlDataReader reader = command.ExecuteReader()) {
                if (reader.HasRows) {
                    while (reader.Read()) {
                        listArray.Add(reader.GetInt32(0));
                    }
                }
                totalAnimal = listArray.Count;
                listArray.Clear();
            }
        }

        private void populateList() {
            MySqlConnection connex = connectToDatabase();
            string sqlString = "SELECT * FROM Animals";
            MySqlCommand command = new MySqlCommand(sqlString, connex);

            using (MySqlDataReader reader = command.ExecuteReader()) {
                if (reader.HasRows) {
                    while (reader.Read()) {
                        switch (reader.GetString(1)) {
                            case "Chien":
                                listArray.Add(new Chien(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetString(5), reader.GetString(6)));
                                break;
                            case "Chat":
                                listArray.Add(new Chat(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetString(5), reader.GetString(6)));
                                break;
                            case "Cheval":
                                listArray.Add(new Cheval(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetString(5), reader.GetString(6)));
                                break;
                            case "Hamster":
                                listArray.Add(new Hamster(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetString(5), reader.GetString(6)));
                                break;
                            case "Lezard":
                                listArray.Add(new Lezard(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetString(5), reader.GetString(6)));
                                break;
                        }
                    }
                }
            }
        }
        
        private void afficherMenu() {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(" Outil de gestion des pensionnaires");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(" 1 - Ajouter un animal");
            Console.WriteLine(" 2 - Afficher la liste de tous les animaux en pension");
            Console.WriteLine(" 3 - Afficher la liste de tous les propriétaires");
            Console.WriteLine(" 4 - Voir le nombre total d'animaux en pension");
            Console.WriteLine(" 5 - Voir le poids total de tous les animaux en pension");
            Console.WriteLine(" 6 - Voir la liste des animaux d’une couleur");
            Console.WriteLine(" 7 - Retirer un animal de la liste");
            Console.WriteLine(" 8 - Modifier un animal de la liste");
            Console.WriteLine(" 9 - Quitter");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine(" Choisissez une option");
            Console.WriteLine("");

            string laSelection = Console.ReadLine();
            selectChoice(laSelection);
        }

        private void startTheMachine() {
            checkList();
            activityLoop();
        }

        // Fonction qui reformat un string .. 1 lettre capitale et le reste minuscule
        private string reformatInput(string strInput) {
            string lowerStr = strInput.ToLower();
            char[] a = lowerStr.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }

        private void selectChoice(string choice) {
            switch (choice) {
                case "1":
                    ajouterUnAnimal();
                    break;
                case "2":
                    voirListeAnimaux();
                    break;
                case "3":
                    voirListePropriétaire();
                    break;
                case "4":
                    voirNombreTotal();
                    break;
                case "5":
                    voirPoidsTotal();
                    break;
                case "6":
                    extraireSelonCouleur();
                    break;
                case "7":
                    retirerUnAnimal();
                    break;
                case "8":
                    //modifierUnAnimal();
                    break;
                case "9":
                    quitApp();
                    break;
                default:
                    //Afficher erreur et retour au menu
                    afficherMessageErreur();
                    break;
            }
        }

        private void afficherMessageErreur() {
            Console.Clear();
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(" Le choix n'est pas valide.");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine(" Appuyez sur une touche pour continuer");

            Console.ReadKey();
        }

        private void ajouterUnAnimal() {

            if (totalAnimal >= 30) {
                Console.Clear();
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine(" Désolé, nous ne pouvons accepter de nouveau pensionnaire.");
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("");
                Console.WriteLine(" Appuyez sur une touche pour continuer");

                Console.ReadKey();
            } else {
                queryAjout();
            }
        }

        private void queryAjout() {
            ArrayList newAnimal = new ArrayList();
            string input;

            newAnimal.Add(nextID);

            Console.Clear();
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(" Le type d'animal");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine(" Entrer le type d'animal..");
            Console.WriteLine("");

            input = Console.ReadLine();
            if (input != "") {
                newAnimal.Add(reformatInput(input));
            }

            Console.Clear();
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(" Le nom de l'animal");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine(" Entrer le nom de l'animal..");
            Console.WriteLine("");

            input = Console.ReadLine();
            if (input != "") {
                newAnimal.Add(reformatInput(input));
            }

            Console.Clear();
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(" L'âge de l'animal");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine(" Entrer l'âge de l'animal..");
            Console.WriteLine("");

            input = Console.ReadLine();
            newAnimal.Add(Int32.Parse(input));

            Console.Clear();
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(" Le poids de l'animal");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine(" Entrer le poids de l'animal..");
            Console.WriteLine("");

            input = Console.ReadLine();
            newAnimal.Add(Int32.Parse(input));

            Console.Clear();
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(" La couleur de l'animal");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine(" Entrer la couleur de l'animal..");
            Console.WriteLine("");

            input = Console.ReadLine();
            if (input != "") {
                newAnimal.Add(reformatInput(input));
            }

            Console.Clear();
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(" Le nom du propriétaire");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine(" Entrer le nom du propriétaire..");
            Console.WriteLine("");

            input = Console.ReadLine();
            if (input != "") {
                newAnimal.Add(reformatInput(input));
            }

            validationAjout(newAnimal);
        }

        private void validationAjout(ArrayList xInput) {

            Console.Clear();
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(" Type" + "\t\t" + "Nom" + "\t\t" + "Âge" + "\t\t" + "Poids" + "\t\t" + "Couleur" + "\t\t" + "Propriétaire");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(" " + xInput[1] + "\t\t" + xInput[2] + "\t\t" + xInput[3] + "\t\t" + xInput[4] + "\t\t" + xInput[5] + "\t\t" + xInput[6]);
            Console.WriteLine("");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine(" Est-ce que ces informations sont valides? [O ou N]");
            Console.WriteLine("");

            // Boucle de validation que je reçois O ou N
            var validated = 0;

            while (validated != 1) {
                var theKey = Console.ReadKey(true);
                if (theKey.Key == ConsoleKey.O) {
                    traiterAjout(xInput);
                    validated = 1;
                } else if (theKey.Key == ConsoleKey.N) {
                    recommencerAjout();
                    validated = 1;
                }
            }
        }

        private void traiterAjout(ArrayList validInput) {

            MySqlConnection connex1 = connectToDatabase();
            string sqlString = "INSERT INTO Animals (type, nom, age, poids, couleur, nomProprio)" + "VALUES (@type, @nom, @age, @poids, @couleur, @nomProprio)";

            MySqlCommand command = new MySqlCommand(sqlString, connex1);
            command.Parameters.AddWithValue("@type", validInput[1]);
            command.Parameters.AddWithValue("@nom", validInput[2]);
            command.Parameters.AddWithValue("@age", validInput[3]);
            command.Parameters.AddWithValue("@poids", validInput[4]);
            command.Parameters.AddWithValue("@couleur", validInput[5]);
            command.Parameters.AddWithValue("@nomProprio", validInput[6]);

            command.ExecuteReader();
            connex1.Close();

            //totalAnimal += 1;
            //nextID += 1;

            Console.WriteLine("");
            Console.WriteLine(" [Animal ajouté au registre]");
            Console.WriteLine("");
            Console.WriteLine(" Appuyez sur une touche pour continuer");

            Console.ReadKey();
        }

        private void recommencerAjout() {
            Console.WriteLine("");
            Console.WriteLine(" Recommençons la saisi..");
            Console.WriteLine("");
            Console.WriteLine(" Appuyez sur une touche pour continuer");

            Console.ReadKey();
            ajouterUnAnimal();
        }

        private void voirListeAnimaux() {

            populateList();

            if (totalAnimal == 0) {
                Console.Clear();
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine(" Nous n'avons présentement aucun pensionnaire.");
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("");
                Console.WriteLine(" Appuyez sur une touche pour continuer");

                Console.ReadKey();
            } else {
                Console.Clear();
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine(" Liste des pensionnaires actuels");
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine(" ID" + "\t\t" + "Type" + "\t\t" + "Nom" + "\t\t" + "Âge" + "\t\t" + "Poids" + "\t\t" + "Couleur" + "\t\t" + "Propriétaire");
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                // Affichage a partir des infos de la BD
                foreach(Animal animal in listArray) {
                    Console.WriteLine(" " + animal.Id + "\t\t" + animal.Type + "\t\t" + animal.Nom + "\t\t" + animal.Age + "\t\t" + animal.Poids + "\t\t" + animal.Couleur + "\t\t" + animal.NomProprio);
                }
                Console.WriteLine("");
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("");
                Console.WriteLine(" Appuyez une touche pour continuer");

                Console.ReadKey();
            }
            listArray.Clear();
        }

        private void voirListePropriétaire() {

            populateList();

            Console.Clear();
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(" Liste des propriétaires de nos pensionnaires");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(" ID" + "\t\t" + "Propriétaire");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            foreach (Animal animal in listArray) {
                Console.WriteLine(" " + animal.Id + "\t\t" + animal.NomProprio);
            }
            Console.WriteLine("");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine(" Appuyez une touche pour continuer");

            Console.ReadKey();
            listArray.Clear();
        }

        private void voirNombreTotal() {

            populateList();

            Console.Clear();
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(" Nombre d'animaux");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(" Nous avons " + listArray.Count + " pensionnaires");
            Console.WriteLine("");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine(" Appuyez une touche pour continuer");

            Console.ReadKey();
            listArray.Clear();
        }

        private void voirPoidsTotal() {

            populateList();

            var poidsTotal = 0;
            foreach (Animal animal in listArray) {
                poidsTotal += animal.Poids;
            }

            Console.Clear();
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(" Poids total de nos pensionnaires");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(" Le poids total de nos pensionnaires est de " + poidsTotal + " kg");
            Console.WriteLine("");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine(" Appuyez une touche pour continuer");

            Console.ReadKey();
            listArray.Clear();
        }

        private void extraireSelonCouleur() {

            populateList();

            string searchColor;
            int colorCount = 0;

            Console.Clear();
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(" Recherche par couleur");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine(" Entrer la couleur à rechercher..");
            Console.WriteLine("");

            string input = Console.ReadLine();
            searchColor = reformatInput(input);


            Console.Clear();
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(" Liste des animaux en pension de couleur " + searchColor);
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(" ID" + "\t\t" + "Type" + "\t\t" + "Nom" + "\t\t" + "Couleur");

            foreach (Animal animal in listArray) {
                if (searchColor == animal.Couleur) {
                    Console.WriteLine(" " + animal.Id + "\t\t" + animal.Type + "\t\t" + animal.Nom + "\t\t" + animal.Couleur);
                    colorCount += 1;
                }
            }
            if (colorCount < 1) {
                Console.WriteLine("");
                Console.WriteLine(" Nous n'avons aucun animal de la couleur " + searchColor);
            }

            Console.WriteLine("");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine(" Appuyez une touche pour continuer");

            Console.ReadKey();
            listArray.Clear();
        }

        private void retirerUnAnimal() {

            populateList();

            if (listArray.Count > 0) {
                Console.Clear();
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine(" Retirer un animal de la liste");
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine(" ID" + "\t\t" + "Type" + "\t\t" + "Nom" + "\t\t" + "Âge" + "\t\t" + "Poids" + "\t\t" + "Couleur" + "\t\t" + "Propriétaire");
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                // Affichage a partir des infos de la BD
                foreach (Animal animal in listArray) {
                    Console.WriteLine(" " + animal.Id + "\t\t" + animal.Type + "\t\t" + animal.Nom + "\t\t" + animal.Age + "\t\t" + animal.Poids + "\t\t" + animal.Couleur + "\t\t" + animal.NomProprio);
                }
                Console.WriteLine("");
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("");
                Console.WriteLine(" Entrer l'ID de l'animal à retirer de la liste..");
                Console.WriteLine("");

                string IDtoDelete = Console.ReadLine();

                retirerID(IDtoDelete);
            } else {
                Console.Clear();
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine(" Il n'y a aucun pensionnaire présentement");
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("");
                Console.WriteLine(" Appuyez sur une touche pour continuer");

                Console.ReadKey();
                listArray.Clear();
            }
        }

        private void retirerID(string theIDtoDelete) {

            int theDeletion = -1;
            Int32 intIDtoDelete = Int32.Parse(theIDtoDelete);

            // Retrouver quel animal dans la liste est associé à l'ID à éliminer
            foreach (Animal animal in listArray) {
                if (intIDtoDelete == animal.Id) {
                    theDeletion = 1;
                    break;
                }
            }
            listArray.Clear();

            if (theDeletion == -1) {
                afficherMessageErreur();
            } else {
                retirerID2(intIDtoDelete);

                Console.WriteLine("");
                Console.WriteLine(" [L'animal lié à l'ID " + theIDtoDelete + " a été retiré du registre]");
                Console.WriteLine("");
                Console.WriteLine(" Appuyez sur une touche pour continuer");

                Console.ReadKey();
            }
            
        }

        private void retirerID2(Int32 toDelete) {
            MySqlConnection connex1 = connectToDatabase();
            string sqlString = "DELETE FROM Animals WHERE id = @id";

            MySqlCommand command = new MySqlCommand(sqlString, connex1);
            command.Parameters.AddWithValue("@id", toDelete);

            command.ExecuteReader();
            connex1.Close();
        }

        private MySqlConnection connectToDatabase() {
            MySqlConnection connex;
            string connectionString = "server=localhost;database=vet_bd;uid=root;pwd=;";
            connex = new MySqlConnection(connectionString);

            try {
                connex.Open();
                if (connex.State == System.Data.ConnectionState.Open) {
                    Console.WriteLine("Connexion à la BD ...");
                }
            }
            catch(Exception ex) {
                Console.WriteLine("Impossible d'ouvrir la connexion. " + ex.Message);
            }
            return connex;
        }

        private void quitApp() {

            Console.Clear();
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(" 8 - Quitter");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");

            Environment.Exit(0);
        }
    }
}
