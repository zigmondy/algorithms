def printMatrix(m, label = "matrix (rows={0}, columns={1}):"):
    print(label.format(len(m), len(m[0])))
    for row in m:
       print(row)