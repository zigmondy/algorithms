import Common;

inputDocument = "The quick brown fox jump over the lazy dog."
documentLength = len(inputDocument)
shingleLength = 10
shingles = []

def createShingles():
    beginIndex = 0;
    endIndex = beginIndex + shingleLength - 1;
    while endIndex <= documentLength:
        shingles.append(inputDocument[beginIndex:endIndex])
        beginIndex = beginIndex + 1
        endIndex = beginIndex + shingleLength - 1;

createShingles()
Common.printVector(shingles)