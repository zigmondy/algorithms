import Common

word1 = ['a', 'b', 'c', 'd', 'a', 'f']
word2 = ['a', 'c', 'b', 'c', 'f']
seq = []

columns = len(word1) + 1
rows = len(word2) + 1
m = [[0 for _ in range(columns)] for _ in range(rows)]

def commonSubsequence():
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
                m[row][column] = max(m[row][column - 1], m[row - 1][column])

def backtrack():
    column = columns - 1
    row = rows - 1
    while column > 0 and row > 0:
        current = m[row][column]
        prevRow = m[row - 1][column]
        prevColumn = m[row][column - 1]
        prevDiagonal = m[row - 1][column - 1]
        if current == prevColumn:
            column = column - 1
        elif current == prevRow:
            row = row - 1
        elif current == prevDiagonal + 1:
            seq.append(word1[column - 1])
            column = column - 1
            row = row - 1

Common.printVector(word1, label="word1")
Common.printVector(word2, label="word2")
commonSubsequence()
Common.printMatrix(m, r="word2", c="word1")
backtrack()
Common.printVector(seq, label="Common Subsequence")