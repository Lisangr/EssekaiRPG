<?php
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "register_db";
$logFile = 'inventory_log.txt';

// Function to log messages to a file
function logMessage($message, $logFile)
{
    $timestamp = date('Y-m-d H:i:s');
    file_put_contents($logFile, "[$timestamp] $message\n", FILE_APPEND);
}

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);

// Check connection
if ($conn->connect_error) {
    logMessage("Connection failed: " . $conn->connect_error, $logFile);
    die("Connection failed: " . $conn->connect_error);
}

logMessage("Connection to database successful", $logFile);

// Get the JSON data from the request
$json = file_get_contents('php://input');
$data = json_decode($json, true);

if ($data === null) {
    logMessage("Failed to decode JSON: " . json_last_error_msg(), $logFile);
    die("Invalid JSON data");
}

logMessage("Received data: " . print_r($data, true), $logFile);

// Extract variables from JSON data
$userId = $data['userId'];
$itemId = $data['itemId'];
$itemName = $data['itemName'];
$quantity = $data['quantity'];
$isRemove = $data['isRemove'];

// Function to add or update item in inventory
function addOrUpdateItem($conn, $userId, $itemId, $itemName, $quantity, $logFile)
{
    $sql = "SELECT id, quantity FROM inventory WHERE userdata_id = ? AND item_id = ?";
    $stmt = $conn->prepare($sql);
    $stmt->bind_param("ii", $userId, $itemId);
    $stmt->execute();
    $result = $stmt->get_result();

    if ($result->num_rows > 0) {
        $row = $result->fetch_assoc();
        // Необходимо обновлять количество предметов до нового значения
        $newQuantity = $quantity; // Используем переданное количество как новое значение
        $updateSql = "UPDATE inventory SET quantity = ? WHERE id = ?";
        $updateStmt = $conn->prepare($updateSql);
        $updateStmt->bind_param("ii", $newQuantity, $row['id']);
        $updateStmt->execute();
        logMessage("Updated item: $itemName, new quantity: $newQuantity", $logFile);
    } else {
        $insertSql = "INSERT INTO inventory (item_id, userdata_id, quantity, itemname) VALUES (?, ?, ?, ?)";
        $insertStmt = $conn->prepare($insertSql);
        $insertStmt->bind_param("iiis", $itemId, $userId, $quantity, $itemName);
        $insertStmt->execute();
        logMessage("Inserted new item: $itemName, quantity: $quantity", $logFile);
    }
}

// Function to remove item from inventory
function removeItem($conn, $userId, $itemId, $logFile)
{
    $sql = "SELECT id, quantity FROM inventory WHERE userdata_id = ? AND item_id = ?";
    $stmt = $conn->prepare($sql);
    $stmt->bind_param("ii", $userId, $itemId);
    $stmt->execute();
    $result = $stmt->get_result();

    if ($result->num_rows > 0) {
        $row = $result->fetch_assoc();
        $newQuantity = $row['quantity'] - 1;

        if ($newQuantity > 0) {
            $updateSql = "UPDATE inventory SET quantity = ? WHERE id = ?";
            $updateStmt = $conn->prepare($updateSql);
            $updateStmt->bind_param("ii", $newQuantity, $row['id']);
            $updateStmt->execute();
            logMessage("Decreased quantity of item ID: $itemId, new quantity: $newQuantity", $logFile);
        } else {
            $deleteSql = "DELETE FROM inventory WHERE id = ?";
            $deleteStmt = $conn->prepare($deleteSql);
            $deleteStmt->bind_param("i", $row['id']);
            $deleteStmt->execute();
            logMessage("Removed item ID: $itemId from inventory", $logFile);
        }
    }
}

// Process request based on action
if ($isRemove) {
    removeItem($conn, $userId, $itemId, $logFile);
} else {
    addOrUpdateItem($conn, $userId, $itemId, $itemName, $quantity, $logFile);
}

logMessage("Request processed successfully", $logFile);

// Close connection
$conn->close();

echo json_encode(["status" => "success"]);
?>
