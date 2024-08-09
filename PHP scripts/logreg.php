<?php
include 'db.php';

$dt = $_POST;

// Add debug information
file_put_contents('debug.log', "Received POST data: " . print_r($dt, true), FILE_APPEND);

$playerInfo = array();
$error = array(
    "errorText" => "empty",
    "isErrored" => false
);

$userData = array(
    "playerInfo" => $playerInfo,
    "error" => $error
);

function SetError($text)
{
    global $userData;
    $userData["playerInfo"] = null;
    $userData["error"]["isErrored"] = true;
    $userData["error"]["errorText"] = $text;
}

if ($dt['type'] == "logining" && isset($dt['login']) && isset($dt['password'])) {
    file_put_contents('debug.log', "Processing login...\n", FILE_APPEND);
    $stmt = $db->prepare("SELECT * FROM `users` WHERE `login` = :login");
    $stmt->bindParam(':login', $dt['login']);
    $stmt->execute();

    if ($stmt->rowCount() > 0) {
        $user = $stmt->fetch(PDO::FETCH_ASSOC);
        if (password_verify($dt['password'], $user['password'])) {
            $stmt = $db->prepare("SELECT * FROM `userdata` WHERE `userID` = :userID");
            $stmt->bindParam(':userID', $user['id']);
            $stmt->execute();
            $userDetails = $stmt->fetchAll(PDO::FETCH_ASSOC);

            $hasCharacters = count($userDetails) > 1; // Check if there is more than one row
            $userData['userID'] = $user['id'];

            if ($hasCharacters) {
                foreach ($userDetails as $details) {
                    if (!empty($details['nickname']) && !empty($details['userMale']) && !empty($details['userClass'])) {
                        $userData['playerInfo'] = array(
                            'hasCharacters' => true,
                            'nickname' => $details['nickname'],
                            'userMale' => $details['userMale'],
                            'userClass' => $details['userClass']
                        );
                        break;
                    }
                }
            } else {
                $userData['playerInfo'] = array(
                    'hasCharacters' => false,
                    'nickname' => '',
                    'userMale' => '',
                    'userClass' => ''
                );
            }

            // Debugging the response data
            file_put_contents('debug.log', "User details: " . print_r($userDetails, true) . "\n", FILE_APPEND);
        } else {
            SetError("Invalid password");
        }
    } else {
        SetError("User not found");
    }
} elseif (isset($dt['type']) && $dt['type'] == "register") {
    if (isset($dt['login']) && isset($dt['password1']) && isset($dt['password2'])) {
        $stmt = $db->prepare("SELECT * FROM `users` WHERE `login` = :login");
        $stmt->bindParam(':login', $dt['login']);
        $stmt->execute();

        if ($stmt->rowCount() == 0) {
            if ($dt['password1'] == $dt['password2']) {
                $hash = password_hash($dt['password1'], PASSWORD_DEFAULT);
                try {
                    $stmt = $db->prepare("INSERT INTO `users`(`login`, `password`) VALUES (:login, :hash)");
                    $stmt->bindParam(':login', $dt['login']);
                    $stmt->bindParam(':hash', $hash);
                    $stmt->execute();
                    $userID = $db->lastInsertId();

                    file_put_contents('debug.log', "Inserted into users: " . $dt['login'] . "\n", FILE_APPEND);

                    $stmt = $db->prepare("INSERT INTO `userdata`(`userID`) VALUES (:userID)");
                    $stmt->bindParam(':userID', $userID, PDO::PARAM_INT);
                    $stmt->execute();

                    file_put_contents('debug.log', "Inserted into userdata: " . $dt['login'] . "\n", FILE_APPEND);
                } catch (PDOException $e) {
                    file_put_contents('debug.log', "Error inserting into data: " . $e->getMessage() . "\n", FILE_APPEND);
                }
            } else {
                SetError("Password Error");
            }
        } else {
            SetError("User Exist");
        }
    }
} else {
    SetError("Error: Unknown data");
}

$json = json_encode($userData, JSON_UNESCAPED_UNICODE);
if (json_last_error() !== JSON_ERROR_NONE) {
    echo json_last_error_msg();
} else {
    echo $json;
}

file_put_contents('debug.log', "Response data: " . print_r($userData, true), FILE_APPEND);
?>
