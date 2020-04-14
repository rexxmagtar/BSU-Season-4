package com.company;

import javafx.application.Application;
import javafx.beans.property.SimpleStringProperty;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.event.ActionEvent;
import javafx.geometry.Insets;
import javafx.scene.Group;
import javafx.scene.Scene;
import javafx.scene.control.*;
import javafx.scene.layout.HBox;
import javafx.scene.layout.VBox;
import javafx.stage.Stage;
import javafx.scene.control.Alert;

import java.sql.*;
import java.util.Optional;

public class AudioshopViewer extends Application {

    private static Connection connection;
    private static Statement statement;
    private static ResultSet resultSet;

    private TableView<Audio> AudiosTable = new TableView<>();
    private TableView<audiotype> typesTable = new TableView<>();

    private static ObservableList<Audio> AudiosData = FXCollections.observableArrayList();
    private static ObservableList<audiotype> amountsData = FXCollections.observableArrayList();

    private final HBox addBox = new HBox(15);

    private int preferNameColumnWidth = 200;
    private int preferAgeColumnWidth = 300;
    private int prefertypeColumnWidth = 150;
    private int preferingTimeColumnWidth = 100;
    private int preferamountColumnWidth = 200;

    public static void main(String[] args) {
        connectionDB();
        createDB();

        launch(args);
        closeConnectionDB();
    }

    private static void connectionDB() {
        try {
            connection = null;
           Class.forName("org.sqlite.JDBC");
            connection = DriverManager.getConnection("jdbc:sqlite:Audioshop.db");

            statement = connection.createStatement();
            String selectSQL = "SELECT * FROM Audios";
            resultSet = statement.executeQuery(selectSQL);

            while (resultSet.next()) {
                Audio Audio = new Audio(resultSet.getString("name"),
                        resultSet.getString("Age"),
                        resultSet.getString("type"),
                        resultSet.getString("ingTime"),
                        resultSet.getString("amount"));
                AudiosData.add(Audio);
            }

            selectSQL = "SELECT * FROM audiotypes";
            resultSet = statement.executeQuery(selectSQL);

            while (resultSet.next()) {
                audiotype audioamount = new audiotype(
                        resultSet.getString("type"),
                        resultSet.getString("id"),
                        resultSet.getString("description")
                );
                amountsData.add(audioamount);
            }


        } catch (Exception e) {
            System.err.println(e.getMessage());
        }
    }

    private static void createDB() {
        try {

            statement.execute("CREATE TABLE IF NOT EXISTS 'audiotypes' " +
                    "('id' INTEGER PRIMARY KEY AUTOINCREMENT,  " +
                    "'type' TEXT, " +
                    "'description' TEXT);");

            statement.execute(
                    "CREATE TABLE IF NOT EXISTS 'Audios' " +
                            "('name' TEXT," +
                            " 'Age' TEXT, " +
                            " 'type' TEXT," +
                            " 'amount' TEXT REFERENCES audiotypes(amount) DEFERRABLE INITIALLY DEFERRED);");

        } catch (Exception e) {
            System.err.println(e.getMessage());
        }
    }

    private static void closeConnectionDB() {
        try {
            resultSet.close();
            connection.close();
        } catch (Exception e) {
            System.out.println(e.getMessage());
        }
    }

    @Override
    public void start(Stage primaryStage) throws Exception {

        Scene scene = new Scene(new Group());
        primaryStage.setTitle("Audioshop table");

        createAudiosTable();
        createatypesTable();
        createAddBox();

        VBox allBox = new VBox(10);
        allBox.setPadding(new Insets(10, 10, 10, 10));
        allBox.getChildren().addAll(new Label("Audioshop table"), AudiosTable, addBox, typesTable);

        ((Group) scene.getRoot()).getChildren().addAll(allBox);

        primaryStage.setScene(scene);
        primaryStage.show();
    }

    private void createAddBox() {
        final TextField addedName = new TextField();
        addedName.setMaxWidth(preferNameColumnWidth);
        addedName.setPromptText("Name");

        final TextField addedAge = new TextField();
        addedAge.setMaxWidth(preferAgeColumnWidth);
        addedAge.setPromptText("Age");

        final TextField addedingTime = new TextField();
        addedingTime.setMaxWidth(preferingTimeColumnWidth);
        addedingTime.setPromptText("Age");


        final TextField addedamount = new TextField();
        addedamount.setMaxWidth(preferamountColumnWidth);
        addedamount.setPromptText("amount");

        ObservableList<String> options =
                FXCollections.observableArrayList(
                        "Aucustic",
                        "CD",
                        "Guitar",
                        "Player"

                );
        final ComboBox<String> typeBox = new ComboBox<>(options);
        typeBox.setPrefWidth(prefertypeColumnWidth + 25);
        typeBox.setPromptText("type");


        final Button addAudioButton = new Button("Add audio");
        addAudioButton.setOnAction((ActionEvent e) -> {
            String name = addedName.getText();
            String Age = addedAge.getText();
            String type = typeBox.getSelectionModel().getSelectedItem();
            String ingTime = addedingTime.getText();
            String amount = addedamount.getText();

            try {
                Audio Audio = new Audio(name, Age, type, ingTime, amount);
                if (!amountExists(type)) {
                    String description = showAddingamountDescriptionDialog(type);
                    audiotype newamount = new audiotype(type, "?", description);
                    addTypeToDataBase(newamount);
                    amountsData.add(newamount);
                }

                AudiosData.add(Audio);
                addAudioToDatabase(name, Age, type, ingTime, amount);

                addedName.clear();
                addedAge.clear();
                addedingTime.clear();
                addedamount.clear();

            } catch (Exception exc) {
                showAddingError(exc.getMessage());
            }
        });

        addBox.getChildren().addAll(addedName, addedAge, typeBox, addedingTime, addedamount,
                addAudioButton);
    }

    private void showAddingError(String message) {
        Alert alert = new Alert(Alert.AlertType.ERROR);
        alert.setTitle("Adding error");
        alert.setHeaderText("Can't add new Audio");
        alert.setContentText(message);
        alert.showAndWait();
    }

    private String showAddingamountDescriptionDialog(String type) {
        TextInputDialog dialog = new TextInputDialog("enter audio type description");
        dialog.setTitle("Adding new audio type");

        dialog.setHeaderText("You have entered new audio type - '" +
                type +
                "'. \nPlease give some description of this: ");
        Optional<String> result = dialog.showAndWait();
        return result.orElse("no description");
    }

    private void addTypeToDataBase(audiotype newtype) {
        String sql = "INSERT INTO audiotypes(amount, description) VALUES(?,?)";
        try (PreparedStatement statement = connection.prepareStatement(sql)) {
            statement.setString(1, newtype.gettype());
            statement.setString(2, newtype.getDescription());

            statement.executeUpdate();
        } catch (Exception e) {
            System.out.println(e.getMessage());
        }
    }

    private boolean amountExists(String newamount) {
        for (audiotype audioamount : amountsData) {
            if (audioamount.gettype().equals(newamount)) {
                return true;
            }
        }
        return false;
    }

    private void createatypesTable() {
        TableColumn<audiotype, String> amountColumn = new TableColumn<>("type");
        TableColumn<audiotype, String> idColumn = new TableColumn<>("ID");
        TableColumn<audiotype, String> descriptionColumn = new TableColumn<>("type description");

        setTypeColumnValues(amountColumn, 200, "type");
        setTypeColumnValues(idColumn, 70, "id");
        setTypeColumnValues(descriptionColumn, 500, "description");

        typesTable.setItems(amountsData);
        typesTable.getColumns().addAll(idColumn, amountColumn, descriptionColumn);
        typesTable.setMaxHeight(200);
    }

    private void createAudiosTable() {
        TableColumn<Audio, String> nameColumn = new TableColumn<>("Name");
        TableColumn<Audio, String> AgeColumn = new TableColumn<>("Age");
        TableColumn<Audio, String> typeColumn = new TableColumn<>("type");

        setAudioColumnValues(nameColumn, preferNameColumnWidth, "name");
        setAudioColumnValues(AgeColumn, preferAgeColumnWidth, "Age");
        setAudioColumnValues(typeColumn, prefertypeColumnWidth, "type");

        TableColumn<Audio, String> amountColumn = new TableColumn<>("amount");
        setAudioColumnValues(amountColumn, preferamountColumnWidth, "amount");


        AudiosTable.setItems(AudiosData);
        AudiosTable.getColumns().addAll(nameColumn, AgeColumn, typeColumn,
                amountColumn);
        AudiosTable.setMaxHeight(300);

    }


    private void setTypeColumnValues(TableColumn<audiotype, String> column, int width, String sqlName) {
        column.setPrefWidth(width);
        column.setCellValueFactory(param -> param.getValue().getFieldBySQL(sqlName));
    }

    private void setAudioColumnValues(TableColumn<Audio, String> column, int width, String sqlName) {
        column.setPrefWidth(width);
        column.setCellValueFactory(param -> param.getValue().getFieldBySQL(sqlName));
    }

    private void addAudioToDatabase(String name, String Age, String type, String ingTime, String amount) {
        String sql = "INSERT INTO Audios(name, Age, type, ingTime, amount) VALUES(?,?,?,?,?)";
        try (PreparedStatement statement = connection.prepareStatement(sql)) {
            statement.setString(1, name);
            statement.setString(2, Age);
            statement.setString(3, type);
            statement.setString(4, ingTime);
            statement.setString(5, amount);

            statement.executeUpdate();
        } catch (Exception e) {
            System.out.println(e.getMessage());
        }
    }

    public static class Audio {
        private SimpleStringProperty name;
        //        private ArrayList<SimpleStringProperty> Age;
        private SimpleStringProperty age;
        private SimpleStringProperty type;
        private SimpleStringProperty amount;

        Audio(String name, String Age, String type,
               String ingTime, String amount) throws Exception {

            if (name == null || name.isEmpty()) {
                throw new Exception("Field 'Name' is empty");
            }
            if (Age == null || Age.isEmpty()) {
                throw new Exception("Field 'Age' is empty");
            }
            if (type == null || type.isEmpty()) {
                throw new Exception("Field 'type' is empty");
            }
            if (ingTime == null || ingTime.isEmpty()) {
                throw new Exception("Field 'ing Age' is empty");
            }
            if (amount == null || amount.isEmpty()) {
                throw new Exception("Field 'amount' is empty");
            }

            this.name = new SimpleStringProperty(name);
            this.type = new SimpleStringProperty(type);
            this.amount = new SimpleStringProperty(amount);
            this.age = new SimpleStringProperty(Age);
            /*
            for (String ingredient : Age.split(",")) {
                this.Age.add(new SimpleStringProperty(ingredient));
            }*/

        }

        SimpleStringProperty getFieldBySQL(String sqlName) {
            SimpleStringProperty res;
            switch (sqlName) {
                case "name":
                    res = this.name;
                    break;
                case "type":
                    res = this.type;
                    break;
                case "amount":
                    res = this.amount;
                    break;
                case "Age":
                    res = this.age;
                    break;
                default:
                    res = null;
            }
            return res;
        }

        public String getName() {
            return name.get();
        }

        public SimpleStringProperty nameProperty() {
            return name;
        }

        public String getAge() {
            return age.get();
        }

        public SimpleStringProperty ageProperty() {
            return age;
        }

        public String gettype() {
            return type.get();
        }

        public SimpleStringProperty typeProperty() {
            return type;
        }

        public String getamount() {
            return amount.get();
        }

        public SimpleStringProperty amountProperty() {
            return amount;
        }
    }

    public static class audiotype {
        private SimpleStringProperty type;
        private SimpleStringProperty id;
        private SimpleStringProperty description;

        audiotype(String type, String id, String description) {
            this.type = new SimpleStringProperty(type);
            this.id = new SimpleStringProperty(id);
            this.description = new SimpleStringProperty(description);
        }

        SimpleStringProperty getFieldBySQL(String sqlName) {
            SimpleStringProperty res;
            switch (sqlName) {
                case "type":
                    res = this.type;
                    break;
                case "id":
                    res = this.id;
                    break;
                case "description":
                    res = this.description;
                    break;
                default:
                    res = null;
            }
            return res;
        }

        public String gettype() {
            return type.get();
        }

        public SimpleStringProperty typeProperty() {
            return type;
        }

        public String getId() {
            return id.get();
        }

        public SimpleStringProperty idProperty() {
            return id;
        }

        public String getDescription() {
            return description.get();
        }

        public SimpleStringProperty descriptionProperty() {
            return description;
        }
    }
}
