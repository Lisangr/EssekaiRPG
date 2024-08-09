<?php
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "register_db";

// Создаем соединение
$conn = new mysqli($servername, $username, $password, $dbname);

// Проверяем соединение
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

$sql = "SELECT id, nickname, lvl, exp, userMale, userClass FROM userdata";
$result = $conn->query($sql);

$data = array();
if ($result->num_rows > 0) {
    while ($row = $result->fetch_assoc()) {
        // Получение данных из таблицы usercustomiser
        $userdata_id = $row['id'];
        $customiser_sql = "SELECT * FROM usercustomiser WHERE userdata_id = $userdata_id";
        $customiser_result = $conn->query($customiser_sql);
        $customiser_data = $customiser_result->fetch_assoc();
        $row['customiser'] = $customiser_data;
        $data[] = $row;
    }
} else {
    echo "0 results";
}
$conn->close();

header('Content-Type: application/json');
echo json_encode(array("characters" => $data));
?>