class LexicalAnalyzer {

    constructor() {
        this.input = "";
        this.state = 0;
        this.currentChar = 0;
    }

}

var lexicalAnalyzer = new LexicalAnalyzer();
var base_url = "localhost";
const statesList = [
    { state: 1, token: "Variable" },
    { state: 3, token: "Entero" },
    { state: 5, token: "Real" },
    { state: 8, token: "Exponente" }
];

let tokens = Array();

var tokenCurrentState = 0;

function getInput() {
    let input = document.getElementById("inputString");

    if (input == null || input.value == "")
        return;

    if (!input.disabled)
        input.disabled = true;

    lexicalAnalyzer.input = input.value;
}

function clearInputTextbox() {
    clearStateAutomata();
    let input = document.getElementById("inputString");
    let charRead = document.getElementById("currentChar");
    let tokenList = document.getElementById("tokenList");

    lexicalAnalyzer = new LexicalAnalyzer();
    tokenCurrentState = 0;

    if (input != null) {
        input.disabled = false;
        input.value = "";
    }

    if (charRead != null)
        charRead.value = "";

    if (tokenList != null)
        tokenList.value = "";
}


async function changeState() {
    const response = await fetch("https://" + base_url + "/getNextState", {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        mode: "cors",
        body: JSON.stringify(lexicalAnalyzer)
    });

    if (!response.ok) {
        console.error("Error: something went wrong!");
        return;
    }

    return await response.json();
}

function updateCurrentCharDisplay() {
    let charRead = document.getElementById("currentChar");

    if (charRead == null)
        return;

    if (lexicalAnalyzer.state != 10)
        charRead.style.color = "green";
    else
        charRead.style.color = "red";

    charRead.value = lexicalAnalyzer.input[lexicalAnalyzer.currentChar - 1];
}

function clearStateAutomata() {
    let outerCircles = document.querySelectorAll("[id^=state_outer_circle_]");
    let innerCircles = document.querySelectorAll("[id^=state_inner_circle_]");

    outerCircles.forEach(outerCircle => {
        if (outerCircle != null && outerCircle.hasAttribute("fill"))
            outerCircle.setAttribute("fill", "#ffffff");
    });

    innerCircles.forEach(innerCircle => {
        if (innerCircle != null && innerCircle.hasAttribute("fill"))
            innerCircle.setAttribute("fill", "#ffffff");
    });
}

function updateStateAutomata() {
    clearStateAutomata();

    if (tokenCurrentState < 0 && tokenCurrentState > 9)
        return;

    let outerCircle = document.getElementById("state_outer_circle_" + tokenCurrentState);

    let innerCircle = document.getElementById("state_inner_circle_" + tokenCurrentState);

    if (outerCircle != null && outerCircle.hasAttribute("fill"))
        outerCircle.setAttribute("fill", "yellow");

    if (innerCircle != null && innerCircle.hasAttribute("fill"))
        innerCircle.setAttribute("fill", "yellow");
}

function updateTokenList() {
    let tokenList = document.getElementById("tokenList");

    if (tokenList == null)
        return;

    statesList.map((element) => {

        if ((element.state == lexicalAnalyzer.state && tokens.length == 0) || (element.state == lexicalAnalyzer.state && tokens[tokens.length - 1].state != element.state)) {
            tokenList.value += element.token + "\n";
            var newToken = { state: element.state, token: element.token };
            tokens.push(newToken);
        }
        tokenCurrentState = lexicalAnalyzer.state;
    });
}

function getNextState() {
    getInput();
    if (lexicalAnalyzer.currentChar < lexicalAnalyzer.input.length) {
        changeState()
            .then((data) => {
                console.log(data);
                lexicalAnalyzer.input = data.input;
                lexicalAnalyzer.currentChar = data.currentChar;
                lexicalAnalyzer.state = data.state;
                updateCurrentCharDisplay();
                updateStateAutomata();
                updateTokenList();
            });
    }
    updateStateAutomata();
}

function sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}

async function analyzeString() {
    getInput();
    await sleep(500);
    while (lexicalAnalyzer.currentChar < lexicalAnalyzer.input.length) {

        const response = await changeState();

        console.log(response);
        if (!response.success) {
            console.error("Error!");
            return;
        }

        console.log("Success!");
        lexicalAnalyzer.input = response.input;
        lexicalAnalyzer.currentChar = response.currentChar;
        lexicalAnalyzer.state = response.state;
        updateCurrentCharDisplay();
        updateStateAutomata();
        updateTokenList();
    }
    updateStateAutomata();
}