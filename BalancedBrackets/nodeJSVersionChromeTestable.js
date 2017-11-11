

process.stdin.resume();
process.stdin.setencoding('ascii');

var input_stdin = "";
var input_stdin_array = "";
var input_currentline = 0;

process.stdin.on('data', function (data) {
    input_stdin += data;
});

process.stdin.on('end', function () {
    input_stdin_array = input_stdin.split("\n");
    main();
});

function readline() {
    return input_stdin_array[input_currentline++];
}

/////////////// ignore above this line ////////////////////

(function () {
    "use strict";

    main();

    function main() {

        const timing = [];
        let inputs = processInput();
        for (let m = 0; m < inputs.length; m++) {
            //performance.mark('checkBalanceBegin');
            let isBalanced = checkBalance(inputs[m], /*measurePerformance*/);
            //measurePerformance('checkBalance');
            let output = convertBoolToOutputString(isBalanced);
            console.log(output);
            //process.stdout.write(output);
        }
        //logPerformance();

        //main scope properties

        //main scope functions
        function processInput() {
            //test
            const rawInputs = [
                "[()][{}()][](){}([{}(())([[{}]])][])[]([][])(){}{{}{[](){}}}()[]({})[{}{{}([{}][])}]"
                , "[()][{}[{}[{}]]][]{}[]{}[]{{}({}(){({{}{}[([[]][[]])()]})({}{{}})})}"
                , "(])[{{{][)[)])(]){(}))[{(})][[{)(}){[(]})[[{}(])}({)(}[[()}{}}]{}{}}()}{({}](]{{[}}(([{]"
                , "){[]()})}}]{}[}}})}{]{](]](()][{))])(}]}))(}[}{{)}{[[}[]"
                , "}(]}){"
                , "((]()(]([({]}({[)){}}[}({[{])(]{()[]}}{)}}]]{({)[}{("
                , "{}{({{}})}[][{{}}]{}{}(){{}[]}{}([[][{}]]())"
                , "(){}[()[][]]{}(())()[[([])][()]{}{}(({}[]()))()[()[{()}]][]]"
                , "()([]({}[]){}){}{()}[]{}[]()(()([[]]()))()()()[]()(){{}}()({[{}][]}[[{{}({({({})})})}]])"
                , "[]([{][][)(])}()([}[}(})}])}))]](}{}})[]({{}}))[])(}}[[{]{}]()[(][])}({]{}[[))[[}[}{(]})()){{(]]){]["
                , "{()({}[[{}]]()(){[{{}{[[{}]{}((({[]}{}()[])))]((()()))}(()[[[]]])((()[[](({([])()}))[]]))}]})}"
                , "()(){{}}[()()]{}{}"
                , "{}()([[]])({}){({[][[][[()]]{{}[[]()]}]})}[](())((())[{{}}])"
                , "{}(((){}){[]{{()()}}()})[]{{()}{(){()(){}}}}{()}({()(()({}{}()((()((([])){[][{()}{}]})))))})"
                , "][[{)())))}[)}}}}[{){}()]([][]){{{{{[)}]]{([{)()][({}[){]({{"
                , "{{}("
                , "{[{((({}{({({()})()})[]({()[[][][]]}){}}))){}}]}{}{({((){{}[][]{}[][]{}}[{}])(())}[][])}"
                , "()[[][()[]][]()](([[[(){()[[]](([]))}]]]))"
                , "()[]({}{})(()){{{}}()()}({[]()}())[](){}(({()}[{}[{({{}}){({}){({})((({()})))}}}]]))"
                , "}[{){({}({)})]([}{[}}{[(([])[(}){[]])([]]}(]]]]{]["
                , "[{]{[{(){[}{}(([(]}])(){[[}(]){(})))}}{{)}}{}][({(}))]}({)"
                , ")})[(]{][[())]{[]{{}}[)[)}[]){}](}({](}}}[}{({()]]"
                , "[[[({[]}({[][[[[][[{(()[][])}()[][]][]{}]]]]}))][(()){}]]]()[{}([]{}){}{{}}]"
                , "({[]({[]})}())[][{}[{{(({{{([{}])}}}))}}]]"
                , "([((()))()])[][][]{}()(([]))[]()[]((){}[]){}(){{}[]}[[{[]}]]"
                , "[[(((({}{[]{}()}){}{{}}){({[]{[{}]{(){}(((){()}))}()}}[[]]()()[()])[[{}{}]()]}))]]{}[]{}({({{}})})"
                , "(]{()}(("
                , "[][(())[({{{()[]}}{[[][[][[[]{{{[()]{{{{}{[]}[][]}}}}}}]]]]}})]]"
                , "}[})})}[)]{}{)"
                , "({(}{})))}(}[)[}{)}}[)[{][{(}{{}]({}{[(})[{[({{[}{(]]})}"
                , "]}})[]))]{][])[}(])]({[]}[]([)"
                , "[{{}{[{{[}[[}([]"
                , "[([]){}][({})({[(([])[][])][[{}{([{{}{(()){{{({}{{}}())}}[]}}()[()[{{{([](()){[[[]]]})}}}]]}])}]]})]"
                , "]{}{(}))}](})[{]]()(]([}]([}][}{]{[])}{{{]([][()){{})[{({{{[}{}](]}}"
                , "{[{}}){(}[][)(}[}][)({[[{]}[(()[}}){}{)([)]}(()))]{)(}}}]["
                , "(]{}{(}}}[)["
                , "[]{}{[[]]}([{}]{}[]){{(())}}"
                , "[)([{(][(){)[)}{)]]}}([((][[}}(]{}]]}]][(({{{))[[){}{]][))[]{]][)[{{}{()]){)])))){{{[(]}[}}{}]"
                , "{({(){[[[][]{}[[([]{})]{}]][[]()()]]}})}[{}{{}}]"
                , ")}][(})){))[{}[}"
                , "{[]{({]}[}}[{([([)([){{}{(}}[]}}[[{[}[[()(])[}[]"
                , "()()()[]"
                , "((){}])][]][}{]{)]]}][{]}[)(])[}[({("
                , ")[((])(]]]]((]){{{{())]}]}(}{([}(({}]])[[{){[}]{{}})[){("
                , "}][[{[((}{[]){}}[[[)({[)}]]}(]]{[)[]}{}(){}}][{()]))})]][(((}}"
                , "([]){}{{}{}}()([([{}{[[]()([(([]()))()[[]]])]}])])"
                , "[()[[]{{[]}()([])}[]][][]][]()[]{}{}[][]{}{}[()(){}]"
                , "{[{){]({(((({](]{([])([{{([])[}(){(]](]{[{[]}}())[){})}))[{})))["
                , "{}[()[]][]{}{}[[{{[[({})]()[[()]]]}}]]"
                , "{[{}[][]]}[((()))][]({})[]{}{()}"
                , "(){[{({})}]}"
                , "([]])][{)]({)[]))}]())[}]))][}{(}}})){]}]{[)}(][})[["
                , "((({{}(([{}(())]))[()]{[[[]()]]}})))"
                , "}()))}(}]]{{})}][{](]][{]{[[]]]}]]}([)({([))[[(]}])}[}(([{)[)]]([[](]}]}{]{{})[]){]}{])("
                , "{}{}{}{[[()]][]}"
                , ")]}]({{})[[[{]{{{}}][))]{{"
                , "))){({}])}])}}]{)()(}(]}(["
                , "([[]][])[[]()][]()(([[]]{[()[]{[][{}]}[()]]{}{[]}}{{}()}(()[([][]{})[[{}][]]{}[]])))"
                , "(]{[({}[){)))}]{[{}][({[({[]))}[}]}{()(([]{]()}})}[]{[)](((]]])([]}}]){)(([]]}[[}["
                , "([[]])({}(([(){{}[{}]}]){[{}]}))[][{}{}](){}"
                , "[][][][][][([])][]{({()}[[()()]{([(){[]{}}{(())}{[](){}()({}())}[({}[[]()])][]])}])}"
                , "}[{{(}})}}(((())()({]{([]((][(({)[({[]]}[])}]{][{{}]{)][}(])}}}))}}}"
                , "[]({})()[]{}{}[]({}{})[]{([])()[()][{()({})[{}{[[()]{}[]][]}(({{[]{()()()}{}[]()}[]}){{}{}})]}]}"
                , "{{(([{)]{}({][{](){({([[[][)}[)})("
                , "[{}]{[()({[{}]})]}"
                , "[[{}]]"
                , "]{{({[{]}[[)]]}{}))}{){({]]}{]([)({{[]){)]{}){){}()})(]]{{])(])[]}][[()()}"
                , "{[([}[[{{(]]][}()())]{){(){)]]){})}]{][][(}[]())[}[)})})[][{[)[})()][]))}[[}"
                , "]()])}[}}}{]]{)[}(}]]])])}{(}{([{]({)]}])(})[{}[)]])]}[]{{)){}{()}]}((}}{({])[}])[]}"
                , "(]}[{}{{][}))){{{([)([[])([]{["
                , "{(()[]){}}(){[]}({{}(()())})([]){}{}(())()[()]{}()"
                , "{{}[{}[{}[]]]}{}({{[]}})[[(){}][]]{}(([]{[][]()()}{{{()()}{[]}({}[]{()})}{()}[[]][()]}))"
                , "{[][]}[{}[](){}]{{}{[][{}]}}"
                , "()(){}(){((){}[])([[]]())}"
                , "{}[[{[((}[(}[[]{{]([(}]][["
                , "{}[([{[{{}()}]{}}([[{}[]]({}{{()}[][][]})])])]"
                , "{[](}([)(])[]]})()]){[({]}{{{)({}(][{{[}}(]{"
                , "[][]{{}[](())}{}({[()]}())[][[][({}([{}]))]]"
                , "((()))[]{[(()({[()({[]}{})]}))]}{[]}{{({}{})[{}{}]{()([()])[{()}()[[]{}()]{}{}[]()]}[[]{[]}([])]}}"
            ]
            //var t = parseInt(readLine());
            //const rawInputs = [];
            //for (var i = 0; i < t; i++) {
            //    var expression = readLine();
            //    rawInputs.Push(expression);
            //}
            return rawInputs;
        }

        //would import this in a real-world setting
        function checkBalance(input, /*measurePerformance*/) {
            //class declerations, can't be hoisted
            class BracketList {
                constructor(arr, leftBracket, rightBracket) {
                    this.arr = arr;
                    this.leftBracket = leftBracket;
                    this.rightBracket = rightBracket;
                }
            }

            //checkBalanceScope properties
            const fullText = input;
            let fullArr = []
                , parenArr = []
                , curlyBracketList = []
                , squareBracketList = []
                , parenBracketList = []
                , individualizedLists = []
                , isDetermined = false
                , isBalanced = null;

            //checkBalanceScope main work
            //check if the input string has even number of characters
            checkSpecialCasesOddCount(fullText.length);
            if (!isDetermined) {
                //initialize remaining scope properties
                init();
                //check each filtered list for even length
                for (let j = 0; j < individualizedLists.length; j++) {
                    if (individualizedLists[j]) { checkSpecialCasesOddCount(individualizedLists[j].length) };
                }
                if (!isDetermined) {
                    //consider running through an individual check on the brackets, based on whether the difference in length is an order of magnitude greater
                    for (let j = 0; j < individualizedLists.length; j++) {
                        //performance.mark('individualAlg' + j + 'Begin');
                        if (individualizedLists[j]
                            && individualizedLists[j + 1]
                            && individualizedLists[j + 1].length / individualizedLists[j].length > 10
                        ) {
                            isBalanced = individualBracketsAlg(individualizedLists[j]);
                            //if (isBalanced !== null) { isDetermined = true };
                        } else if (individualizedLists[j]
                            && individualizedLists[j + 1].length / fullArr.length > 10
                        ) {
                            isBalanced = individualBracketsAlg(individualizedLists[j]);
                            //if (isBalanced !== null) { isDetermined = true };
                        }
                        if (isDetermined) { break; }
                        //measurePerformance('individualAlng' + j);
                    }
                    if (!isDetermined) {
                        //run a check through allBrackets in the fullText
                        //performance.mark('allBracketsAlgBegin');
                        allBracketsAlg(fullArr);
                        //measurePerformance('allBracketsAlg');
                        //if it hasn't been determined false at this point, it's true
                        if (!isDetermined) {
                            isDetermined = true;
                            isBalanced = true;
                        }
                    }
                }
            }
            return isBalanced;

            function init() {
                fullArr = fullText.split("");
                Object.freeze(fullArr);
                //performance.mark('initializeFilteredArraysBegin')
                const squareBrackets = new BracketList(fullArr.filter(i => i === '[' || i === ']'), '[', ']');
                const curlyBrackets = new BracketList(fullArr.filter(i => i === '{' || i === '}'), '{', '}');
                const parenBrackets = new BracketList(fullArr.filter(i => i === '(' || i === ')'), '(', ')');
                //measurePerformance('initializeFilteredArrays');
                //performance.mark('sortFilteredArraysBegin');
                const individualizedLists = [squareBrackets, curlyBrackets, parenBrackets].sort((a, b) => a.length - b.length).reverse();
                //measurePerformance('sortFilteredArrays');
            }

            function checkSpecialCasesOddCount(characterCount) {
                if (characterCount % 2 !== 0) {
                    isDetermined = true;
                    isBalanced = false;
                }
            }

            function individualBracketsAlg(bracketList) {
                let count = 0;
                for (let j = 0; j < bracketList.arr.length; j++) {
                    if (bracketList.arr[j] === bracketList.leftBracket) {
                        count++;
                    } else if (bracketList.arr[j] === bracketList.rightBracket) {
                        count--;
                        if (count < 0) {
                            isBalanced = false;
                            isDetermined = true;
                            break;
                        }
                    }
                }
            }

            function allBracketsAlg(fullArr) {
                //allAlg Scope Properties
                const stacksAndCounts = {
                    squareStack: []
                    , curlyStack: []
                    , parenStack: []
                    , squareCount: 0
                    , curlyCount: 0
                    , parenCount: 0
                }

                //allAlg Scope Work
                for (let j = 0; j < fullArr.length; j++) {
                    switch (fullArr[j]) {
                        case '[':
                            leftBracket("squareCount", "curlyStack", "curlyCount", "parenStack", "parenCount");
                            break;
                        case '{':
                            leftBracket("curlyCount", "squareStack", "squareCount", "parenStack", "parenCount");
                            break;
                        case '(':
                            leftBracket("parenCount", "curlyStack", "curlyCount", "squareStack", "squareCount");
                            break;
                        case ']':
                            rightBracket("squareCount", "squareStack", "curlyCount", "parenCount");
                            break;
                        case '}':
                            rightBracket("curlyCount", "curlyStack", "squareCount", "parenCount");
                            break;
                        case ')':
                            rightBracket("parenCount", "parenStack", "curlyCount", "squareCount");
                            break;
                        default:
                            break;
                    }
                    if (isDetermined) {
                        break;
                    }
                }
                //at the end of the loop, all counts must be at zero
                if (stacksAndCounts.curlyCount !== 0
                    || stacksAndCounts.squareCount !== 0
                    || stacksAndCounts.parenCount !== 0) {
                    isBalanced = false;
                    isDetermined = true;
                }

                //allAlg Scope Functions
                function leftBracket(thisCount, aStack, aCount, bStack, bCount) {
                    stacksAndCounts[thisCount]++;
                    if (stacksAndCounts[aCount] > 0) {
                        stacksAndCounts[aStack].push(aCount);
                        stacksAndCounts[aCount] = 0;
                    }
                    if (stacksAndCounts[bCount] > 0) {
                        stacksAndCounts[bStack].push(bCount);
                        stacksAndCounts[bCount] = 0;
                    }
                }

                function rightBracket(thisCount, thisStack, aCount, bCount) {
                    stacksAndCounts[thisCount]--;
                    //disqualified if closes off before enclosing brackets are closed
                    if (stacksAndCounts[aCount] > 0 || stacksAndCounts[bCount] > 0) {
                        isBalanced = false;
                        isDetermined = true;
                        return;
                        //disqualified by a right bracket when there are no open left brackets of the same kind
                    }
                    else if (stacksAndCounts[thisCount] < 0 && stacksAndCounts[thisStack].length == 0) {
                        isBalanced = false;
                        isDetermined = true;
                        return
                        //if no disqualification, pop the brackets stack
                    } else if (stacksAndCounts[thisCount] < 0)
                        stacksAndCounts[thisCount] += stacksAndCounts[thisStack].pop();
                }
            }
        }

        function convertBoolToOutputString(isBalanced) {
            if (isBalanced === true) {
                return "YES";
            } else if (isBalanced === false) {
                return "NO";
            } else {
                console.error("The isBalanced property is neither true nor false");
            }
        }

        
        function measurePerformance(funcName) {
            performance.mark(funcName + "End");
            performance.measure(funcName, funcName + "Begin", funcName + "End");
            let duration = performance.getEntriesByName(funcName)[0].duration;
            timing.push(funcName + ": " + duration);
        }

        function logPerformance() {
            for (let j = 0; j < timing.length; j++) {
                console.log(timing[j]);
            }
            performance.clearMarks();
            performance.clearMeasures();
        }
        
    }
})();