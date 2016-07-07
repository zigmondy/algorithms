import Common;

inputDocument = "The quick brown fox jumped over the lazy dog."
documentLength = len(inputDocument)
shingleLength = 10
shingles = []

def createShingles():
    beginIndex = 0;
    endSliceIndex = beginIndex + shingleLength;
    while endSliceIndex <= documentLength:
        # Slice ends at n-1 in python
        shingles.append(inputDocument[beginIndex:endSliceIndex])
        beginIndex = beginIndex + 1
        endSliceIndex = beginIndex + shingleLength;

print(documentLength)
createShingles()
Common.printVector(shingles)