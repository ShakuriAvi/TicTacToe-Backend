# TicTacToe-Backend
TicTacToe game implemented by .net on c#. The client side implemented with Winforms and backend side with ASP.NET Core.</br>
Link to Backend side: https://github.com/ShakuriAvi/TicTacToe-Client.</br>


Server side:The site and the connection to the database are on the server side.</br> The model that I worked is MVC (model view controller).</br>
There are many options in Website:
* Register- This is the game registration page. For to start a game on the client side, the user must logs in to the system.
* Game Table- Using queries this page displays tables of:</br>
1) all of the user's games in the table (on what date and result).</br>
2) How many games each player played.</br>
3) All games the player played in descending order.</br>
* Queries- Using queries this page displays tables of:</br>
1) All players with all their details stored in the database.</br>
2) All games with all their details in the database.</br>
* Edit/Delete- On this page you can delete from the database: players and games.</br> In addition you can edit the player data (username and password).</br>

Client side: The game is played on the client side. In every game the players are: Player vs. System.</br>
the user must log in with a username and password with which he registered on the site before he start the game.</br>
After the user logs in to the site by database (Microsoft SQL server) , there are two options: </br>
* view past games- By using the database, the user can see his past games, with a combobox that displays the games by date and game week.</br>
* start a new game - The game includes a 5X5 board and the rules of the game are similar to TicTacToe.</br> When there are 4 sequence similar elements  (X OR O)  in the same row , diagonally or in the column, the game ends and the winner is displayed.</br>

# Register Page

![register](https://user-images.githubusercontent.com/65177459/130361880-d23a1693-1d2b-41de-a566-65283de71a46.png)
# Queries Page

![Queries](https://user-images.githubusercontent.com/65177459/130361879-40102a8b-f2f9-4752-b7f9-4cd8a0564d51.png)

# Game-tables Page

![game-tables](https://user-images.githubusercontent.com/65177459/130361882-255e8f3e-e5c3-46f0-b9ee-1092551bead8.png)

# Delete\Edit Page

![dedit](https://user-images.githubusercontent.com/65177459/130361881-05be18c9-cd2e-4bbf-b583-06903f636695.png)
