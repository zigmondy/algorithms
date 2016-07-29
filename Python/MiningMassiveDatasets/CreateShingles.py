import Common;

shingleLength = 10
def createShingles(inputDocument):
    documentLength = len(inputDocument)
    shingles = []
    hashedShingles = []
    beginIndex = 0;
    endSliceIndex = beginIndex + shingleLength;
    while endSliceIndex <= documentLength:
        # Slice ends at n-1 in python
        shingle = inputDocument[beginIndex:endSliceIndex]
        shingles.append(shingle)
        hashedShingles.append(hash(shingle))
        beginIndex = beginIndex + 1
        endSliceIndex = beginIndex + shingleLength;
    Common.printVector(shingles)
    Common.printVector(hashedShingles)

createShingles("The quick brown fox jumped over the lazy dog.")