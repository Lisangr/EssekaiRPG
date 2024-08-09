<?php
include 'db.php';

$dt = $_POST;

// Add debug information
file_put_contents('debugCharacter.log', "Received POST data: " . print_r($dt, true), FILE_APPEND);

class CharacterInfo
{
    public $userMale;
    public $userClass;
    public $nickname;
    public $id; // Added to hold the ID

    public function __construct($id, $male, $clas, $nick)
    {
        $this->id = $id;
        $this->userMale = $male;
        $this->userClass = $clas;
        $this->nickname = $nick;
    }
}

class CharacterError
{
    public $errorText;
    public $isErrored;

    public function __construct($errorText, $isErrored)
    {
        $this->errorText = $errorText;
        $this->isErrored = $isErrored;
    }
}

class CharacterData
{
    public $characterInfo;
    public $error;

    public function __construct($characterInfo, $error)
    {
        $this->characterInfo = $characterInfo;
        $this->error = $error;
    }
}

// Initialize data
$characterInfo = new CharacterInfo(null, "male", "class", "nickname");
$error = new CharacterError("empty", false);
$characterData = new CharacterData($characterInfo, $error);

if (isset($dt['nickname']) && isset($dt['userMale']) && isset($dt['userClass']) && isset($dt['userID'])) {
    $nickname = $dt['nickname'];
    $userMale = $dt['userMale'];
    $userClass = $dt['userClass'];
    $userID = $dt['userID'];

    // Your query to insert data
    try {
        $stmt = $db->prepare("INSERT INTO `userdata`(`userID`, `userMale`, `userClass`, `nickname`) VALUES (:userID, :userMale, :userClass, :nickname)");
        $stmt->bindParam(':userID', $userID, PDO::PARAM_INT);
        $stmt->bindParam(':userMale', $userMale);
        $stmt->bindParam(':userClass', $userClass);
        $stmt->bindParam(':nickname', $nickname);
        $stmt->execute();

        // Get the last inserted ID
        $lastInsertId = $db->lastInsertId();
        file_put_contents('debugCharacter.log', "Inserted into userdata with ID: " . $lastInsertId . "\n", FILE_APPEND);

        $characterData->characterInfo = new CharacterInfo($lastInsertId, $userMale, $userClass, $nickname);
        $characterData->error = new CharacterError("Registration successful", false);
    } catch (PDOException $e) {
        file_put_contents('debugCharacter.log', "Error inserting into userdata: " . $e->getMessage() . "\n", FILE_APPEND);
        $characterData->error = new CharacterError("Registration failed: " . $e->getMessage(), true);
    }
} else {
    $characterData->error = new CharacterError("Missing required fields", true);
}

// Add debug output
$json = json_encode($characterData, JSON_UNESCAPED_UNICODE);
if (json_last_error() !== JSON_ERROR_NONE) {
    echo json_last_error_msg();
} else {
    echo $json;
}

file_put_contents('debugCharacter.log', print_r($dt, true), FILE_APPEND);
?>