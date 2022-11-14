class LexicalAnalyzer {

    constructor() {
        this.input = "";
        this.state = 0;
        this.currentChar = 0;
    }

}

var lexicalAnalyzer = new LexicalAnalyzer();
var base_url = "";

function getInput() {
    let input = document.getElementById("inputString");

    if (input == null || input.value == "")
        return;

    lexicalAnalyzer.input = input.value;
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

function getNextState() {
    getInput();
    changeState()
        .then((data) => {
            console.log(data);
            lexicalAnalyzer.input = data.input;
            lexicalAnalyzer.currentChar = data.currentChar;
            lexicalAnalyzer.state = data.state;
        });
}

function sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}

async function analyzeString() {
    getInput();
    await sleep(500);
    while (lexicalAnalyzer.currentChar != lexicalAnalyzer.input.length) {

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
    }
}