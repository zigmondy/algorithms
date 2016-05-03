import Common

p = [5, 4, 6, 2, 7]
length = len(p) - 1
m = [[0 for _ in range(length)] for _ in range(length)]
s = [[0 for _ in range(length)] for _ in range(length)]

def computeMatrix():
    for currentLength in range(length):
        print("Length={0}".format(currentLength))
        for i in range(0, length - currentLength, 1):
            j = i + currentLength
            print("{0},{1}".format(i, j))
            for k in range(i, j, 1):
                print("   k={0}".format(k))
                num = m[i][k] + m[k+1][j] + p[i]*p[k + 1]*p[j + 1]
                if (m[i][j] == 0 or num < m[i][j]):
                    m[i][j] = num
                    s[i][j] = k

Common.printVector(p, label="Vector p")
computeMatrix()
Common.printMatrix(m, label="Optimal Cost", r="Row Size", c="Column Size")
Common.printMatrix(s, label="Partition", r="Row Size", c="Column Size")