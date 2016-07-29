import Common

word1 = ['a', 'b', 'c', 'd', 'a', 'f']
word2 = ['z', 'b', 'c', 'd', 'f']
seq = []

columns = len(word1) + 1
rows = len(word2) + 1
m = [[0 for _ in range(columns)] for _ in range(rows)]

def longestCommon():
    for row in range(1, rows):
        for column in range(1, columns):
            char1 = word1[column - 1]
            char2 = word2[row - 1]
            if char1 == char2:
                # Diagonal + 1
                diagonal = m[row - 1][column - 1]
                m[row][column] = diagonal + 1
            else:
                # Max of previous column or row
                m[row][column] = 0

def backtrack():
    # Find the max element
    maxValue = 0
    maxRowIndex = 0
    maxColumnIndex = 0
    for row in range(1, rows):
        rowMax = max(m[row])
        if rowMax > maxValue:
            maxValue = rowMax
            maxRowIndex = row
            maxColumnIndex = m[row].index(maxValue)
    
    while maxRowIndex > 0 and maxColumnIndex > 0:
        diagonal = m[maxRowIndex - 1][maxColumnIndex - 1]
        if diagonal == maxValue - 1:
            seq.insert(0, word1[maxColumnIndex - 1])
            maxValue = diagonal
            maxRowIndex = maxRowIndex - 1
            maxColumnIndex = maxColumnIndex - 1
        else:
            break;

Common.printVector(word1, label="word1")
Common.printVector(word2, label="word2")
longestCommon()
Common.printMatrix(m, r="word2", c="word1")
backtrack()
Common.printVector(seq, label="Common Substring")