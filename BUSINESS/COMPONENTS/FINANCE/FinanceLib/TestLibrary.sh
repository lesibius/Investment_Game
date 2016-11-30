ValueOperator=$(echo "ValueOperator/*.cs")
Investment=$(echo "Investment/*.cs")
Measurement=$(echo "Measurement/*.cs")
TestFile=$(echo "Test/TestFile.cs")
dll=$(echo "-r:System.Drawing.dll -r:System.Windows.Forms.DataVisualization.dll")

mcs -out:Test/TestFile.exe $TestFile $ValueOperator $Investment $Measurement $dll
