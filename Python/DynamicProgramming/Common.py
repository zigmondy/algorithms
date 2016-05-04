def printMatrix(m, label="Matrix", r="", c="", symbol=""):
    message = "{0} - {1} x {2} - {3} x {4}:".format(label, len(m), len(m[0]), r, c)
    printBanner(len(message)) 
    print(message)
    printBanner(len(message))
    for rowIndex in range(len(m)):
        buffer = ""
        for columnIndex in range(len(m[0])):
	        buffer = buffer + "{0}{1} ".format(symbol, m[rowIndex][columnIndex])
        buffer = "[" + buffer.rstrip() + "]"
        print(buffer)

def printVector(v, label="Vector"):
    message = "{0} - {1}".format(label, len(v))
    printBanner(len(message))
    print(message)
    printBanner(len(message))    
    print(v)

def printBanner(l):
    buffer = "-"
    for i in range(l):
        buffer = buffer + "-"
    print(buffer)    
