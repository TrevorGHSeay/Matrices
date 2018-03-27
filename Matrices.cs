using System;

namespace Matrices
{

    /// <summary>
    /// The primary class for matrix manipulations and transformations.
    /// </summary>
    public sealed class Matrix
    {

        //Static Randomizer for Matrix.Randomize() Method
        private static Random _random = new Random();

        // Main Data
        private float[][] _matrix;

        // Index Accessors
        public float this[int col, int row]
        {
            get { return _matrix[col][row]; }
            set { _matrix[col][row] = value; }
        }
        public float[] this[int col]
        {
            get { return _matrix[col]; }
            set
            {
                if (value.Length == this.Rows)
                    _matrix[col] = value;
                else
                    throw new Exception("Length of the array must match the number of rows.");
            }
        }

        /// <summary>
        /// Sums two Matrices.
        /// </summary>
        public static Matrix operator +(Matrix A, Matrix B)
        {
            return Matrix.Add(A, B);
        }

        /// <summary>
        /// Adds a value to each element in a matrix.
        /// </summary>
        public static Matrix operator +(Matrix A, float B)
        {
            return Matrix.Add(A, B);
        }

        /// <summary>
        /// Adds a value to each element in a Matrix.
        /// </summary>
        public static Matrix operator +(float A, Matrix B)
        {
            return Matrix.Add(B, A);
        }

        /// <summary>
        /// Subtracts the second Matrix from the first.
        /// </summary>
        public static Matrix operator -(Matrix A, Matrix B)
        {
            return Matrix.Subtract(A, B);
        }

        /// <summary>
        /// Subtracts a value from each element in a Matrix.
        /// </summary>
        public static Matrix operator -(Matrix A, float B)
        {
            return Matrix.Subtract(A, B);
        }
        
        /// <summary>
        /// Creates the Hadamard product of two Matrices.
        /// </summary>
        public static Matrix operator ^(Matrix A, Matrix B)
        {
            return Matrix.Hadamard(A, B);
        }

        /// <summary>
        /// Creates the cross product of two matrices.
        /// </summary>
        public static Matrix operator *(Matrix A, Matrix B)
        {
            return Matrix.Cross(A, B);
        }

        /// <summary>
        /// Multiplies every element of a Matrix with a value.
        /// </summary>
        public static Matrix operator *(Matrix A, float B)
        {
            return Matrix.Multiply(A, B);
        }
        
        // Equality Operator
        public static bool operator ==(Matrix A, Matrix B)
        {
            return A.Equals(B);
        }
        // Inequality Operator
        public static bool operator !=(Matrix A, Matrix B)
        {
            return !A.Equals(B);
        }

        /// <summary>
        /// The number of columns this Matrix has.
        /// </summary>
        public int Columns
        {
            get
            {
                return _matrix.Length;
            }
        }

        /// <summary>
        /// The number of rows this Matrix has.
        /// </summary>
        public int Rows
        {
            get
            {
                return _matrix[0].Length;
            }
        }

        /// <summary>
        /// Creates a new Matrix of height and width.
        /// </summary>
        /// <param name="Columns">The height of the new Matrix.</param>
        /// <param name="Rows">The width of the new Matrix.</param>
        public Matrix(int Columns, int Rows)
        {
            _matrix = new float[Columns][];
            for (int i = 0; i < Columns; i += 1)
                _matrix[i] = new float[Rows];
        }

        /// <summary>
        /// Creates a new Matrix from an array.
        /// </summary>
        /// <param name="Array">The array of values to use.</param>
        /// <param name="IsColumn">Denotes whether or not the Matrix is oriented vertically.</param>
        public Matrix(float[] Array, bool IsColumn)
        {

            if (IsColumn)
            {
                _matrix = new float[Array.Length][];
            }
            else
            {
                _matrix = new float[1][];
                _matrix[0] = new float[Array.Length];
            }

            for (int i = 0; i < Array.Length; i += 1)
            {
                if (IsColumn)
                {
                    _matrix[i] = new float[1];
                    _matrix[i][0] = Array[i];
                }
                else
                    _matrix[0][i] = Array[i];
            }

        }

        /// <summary>
        /// Creates a Matrix from a 2D array.
        /// </summary>
        /// <param name="Array">The array of values to use.</param>
        public Matrix(float[,] Array)
        {
            _matrix = new float[Array.GetLength(0)][];
            for (int i = 0; i < Array.GetLength(0); i += 1)
            {
                _matrix[i] = new float[Array.GetLength(1)];
                for (int j = 0; j < Array.GetLength(1); j += 1)
                    _matrix[i][j] = Array[i, j];
            }
        }

        /// <summary>
        /// Converts this instance to string-table form.
        /// </summary>
        new public string ToString()
        {
            string result = "";
            for (int i = 0; i < Columns; i += 1)
            {
                for (int j = 0; j < Rows; j += 1)
                    result += _matrix[i][j].ToString() + " ";
                if (i != Columns - 1)
                    result += Environment.NewLine;
            }
            return result;
        }

        /// <summary>
        /// Randomizes all indices to a value between -1 and 1.
        /// </summary>
        public void Randomize()
        {
            for (int i = 0; i < Rows; i += 1)
            {
                for (int j = 0; j < Columns; j += 1)
                    _matrix[j][i] = (float)(_random.NextDouble() * 2f) - 1f;
            }
        }

        /// <summary>
        /// Transposes the matrix.
        /// </summary>
        public void Transpose()
        {
            Matrix spr = new Matrix(this.Rows, this.Columns);
            for (int i = 0; i < Columns; i += 1)
            {
                for (int j = 0; j < Rows; j += 1)
                    spr._matrix[j][i] = _matrix[i][j];
            }
            this._matrix = spr._matrix;
        }

        /// <summary>
        /// Returns the transposed version of Input.
        /// </summary>
        /// <param name="Matrix"></param>
        public static Matrix Transpose(Matrix Matrix)
        {
            Matrix result = new Matrix(Matrix.Rows, Matrix.Columns);
            for (int i = 0; i < Matrix.Columns; i += 1)
            {
                for (int j = 0; j < Matrix.Rows; j += 1)
                    result._matrix[j][i] = Matrix._matrix[i][j];
            }
            return result;
        }

        /// <summary>
        /// Adds Value to each index.
        /// </summary>
        public void Add(float Value)
        {
            int rowMax = Rows - 1;
            for (int i = 0; i != Columns; i += 1)
            {
                for (int j = 0; j < rowMax; j += 2)
                {
                    _matrix[i][j + 0] += Value;
                    _matrix[i][j + 1] += Value;
                }
                _matrix[i][rowMax] += Value;
            }
        }

        /// <summary>
        /// Returns the result of adding Value to each index in Matrix.
        /// </summary>
        /// <param name="Matrix">The Matrix in which to add Value to each index of.</param>
        /// <param name="Value">The value to add to each index of Matrix.</param>
        public static Matrix Add(Matrix Matrix, float Value)
        {
            Matrix result = new Matrix(Matrix.Columns, Matrix.Rows);
            int rowMax = Matrix.Rows - 1;
            for (int i = 0; i != Matrix.Columns; i += 1)
            {
                for (int j = 0; j < rowMax; j += 2)
                {
                    result._matrix[i][j + 0] = Matrix._matrix[i][j + 0] + Value;
                    result._matrix[i][j + 1] = Matrix._matrix[i][j + 1] + Value;
                }
                result._matrix[i][rowMax] = Matrix._matrix[i][rowMax] + Value;
            }
            return result;
        }

        /// <summary>
        /// Sums this object with Matrix.
        /// </summary>
        public void Add(Matrix Matrix)
        {
            if (this.Columns == Matrix.Columns && this.Rows == Matrix.Rows)
            {
                for (int i = 0; i < Columns; i += 1)
                    for (int j = 0; j < Rows; j += 1)
                        _matrix[i][j] += Matrix._matrix[i][j];
            }
            else
                throw new Exception("When calculating the sum of two Matrices, the dimensions of both must be equal.");
        }

        /// <summary>
        /// Returns the result of adding Matrix A to Matrix B.
        /// </summary>
        public static Matrix Add(Matrix A, Matrix B)
        {
            if (A.Columns == B.Columns && A.Rows == B.Rows)
            {
                Matrix result = new Matrix(A.Columns, A.Rows);
                for (int i = 0; i < A.Columns; i += 1)
                    for (int j = 0; j < A.Rows; j += 1)
                        result._matrix[i][j] = A._matrix[i][j] + B._matrix[i][j];
                return result;
            }
            else
                throw new Exception("When calculating the sum of two Matrices, the dimensions of both must be equal.");
        }

        /// <summary>
        /// Subtracts Value from each index in this object.
        /// </summary>
        public void Subtract(float Value)
        {
            for (int i = 0; i < Columns; i += 1)
                for (int j = 0; j < Rows; j += 1)
                    _matrix[i][j] -= Value;
        }

        /// <summary>
        /// Returns the result of subtracting Value from each index in Matrix.
        /// </summary>
        /// <param name="Matrix">The Matrix in which to subtract Value from each index of.</param>
        /// <param name="Value">The value to add to each index of Matrix.</param>
        public static Matrix Subtract(Matrix Matrix, float Value)
        {
            Matrix result = new Matrix(Matrix.Columns, Matrix.Rows);
            for (int i = 0; i < Matrix.Columns; i += 1)
                for (int j = 0; j < Matrix.Rows; j += 1)
                    result._matrix[i][j] = Matrix._matrix[i][j] - Value;
            return result;
        }

        /// <summary>
        /// Subtracts Matrix from this object.
        /// </summary>
        public void Subtract(Matrix Matrix)
        {
            if (this.Columns == Matrix.Columns && this.Rows == Matrix.Rows)
            {
                for (int i = 0; i < Matrix.Columns; i += 1)
                    for (int j = 0; j < Matrix.Rows; j += 1)
                        _matrix[i][j] -= Matrix._matrix[i][j];
            }
            else
                throw new Exception("When calculating the sum of two Matrices, the dimensions of both must be equal.");
        }

        /// <summary>
        /// Returns the result of subtracting Matrix B from Matrix A.
        /// </summary>
        public static Matrix Subtract(Matrix A, Matrix B)
        {
            if (A.Columns == B.Columns && A.Rows == B.Rows)
            {
                Matrix result = new Matrix(A.Columns, A.Rows);
                for (int i = 0; i < A.Columns; i += 1)
                    for (int j = 0; j < A.Rows; j += 1)
                        result._matrix[i][j] = A._matrix[i][j] - B._matrix[i][j];
                return result;
            }
            else
                throw new Exception("When calculating the sum of two Matrices, the dimensions of both must be equal.");
        }

        /// <summary>
        /// Multiplies this object by Value.
        /// </summary>
        public void Multiply(float Value)
        {
            for (int i = 0; i < Columns; i += 1)
                for (int j = 0; j < Rows; j += 1)
                    _matrix[i][j] *= Value;
        }

        /// <summary>
        /// Returns the result of multiplying Matrix by Value.
        /// </summary>
        /// <param name="Matrix">The Matrix that is to be multiplied by Value.</param>
        /// <param name="B">The value in which to multiply each index of Matrix by.</param>
        public static Matrix Multiply(Matrix Matrix, float Value)
        {
            Matrix result = new Matrix(Matrix.Columns, Matrix.Rows);
            for (int i = 0; i < Matrix.Columns; i += 1)
                for (int j = 0; j < Matrix.Rows; j += 1)
                    result._matrix[i][j] = Matrix._matrix[i][j] * Value;
            return result;
        }

        /// <summary>
        /// Crosses this object with Matrix A and creates the dot product of the two.
        /// </summary>
        public void Cross(Matrix A)
        {
            if (this.Columns == A.Rows)
            {
                float ji = 0f;
                Matrix result = new Matrix(A.Columns, this.Rows);
                for (int i = 0; i < this.Rows; i += 1)
                {
                    for (int j = 0; j < A.Columns; j += 1)
                    {
                        ji = 0f;
                        for (int k = 0; k < this.Columns; k += 1)
                            ji += _matrix[k][i] * A._matrix[j][k];
                        result._matrix[j][i] = ji;
                    }
                }
                this._matrix = result._matrix;
            }
            else
                throw new Exception("When calculating the cross product of two Matrices, the number of columns in Matrix A must be equal to the number of rows in Matrix B." + Environment.NewLine + "Dimensions of this: " + this.Columns + "x" + this.Rows + Environment.NewLine + "Dimensions of A: " + A.Columns + "x" + A.Rows);
        }

        /// <summary>
        /// Returns the dot product of A and B.
        /// </summary>
        public static Matrix Cross(Matrix A, Matrix B)
        {
            if (A.Columns == B.Rows)
            {
                float ji = 0f;
                Matrix result = new Matrix(B.Columns, A.Rows);
                for (int i = 0; i < A.Rows; i += 1)
                {
                    for (int j = 0; j < B.Columns; j += 1)
                    {
                        ji = 0f;
                        for (int k = 0; k < A.Columns; k += 1)
                            ji += A._matrix[k][i] * B._matrix[j][k];
                        result._matrix[j][i] = ji;
                    }
                }
                return result;
            }
            else
                throw new Exception("When calculating the cross product of two Matrices, the number of columns in Matrix A must be equal to the number of rows in Matrix B." + Environment.NewLine + "Dimensions of A: " + A.Columns + "x" + A.Rows + Environment.NewLine + "Dimensions of B: " + B.Columns + "x" + B.Rows);
        }

        /// <summary>
        /// Multiplies this Matrix with A, element-wise.
        /// </summary>
        public void Hadamard(Matrix A)
        {
            if (this.Columns == A.Columns && this.Rows == A.Rows)
            {
                for (int i = 0; i < this.Columns; i += 1)
                {
                    for (int j = 0; j < this.Rows; j += 1)
                        this._matrix[i][j] *= A._matrix[i][j];
                }
            }
            else
                throw new Exception("When calculating the hadamard product of two Matrices, the dimensions of both must be equal.");
        }

        /// <summary>
        /// Returns the the result of multiplying both Matrices element-wise.
        /// </summary>
        public static Matrix Hadamard(Matrix A, Matrix B)
        {
            if (A.Columns == B.Columns && A.Rows == B.Rows)
            {
                Matrix result = new Matrix(A.Columns, A.Rows);
                for (int i = 0; i < A.Columns; i += 1)
                {
                    for (int j = 0; j < A.Rows; j += 1)
                        result._matrix[i][j] = A._matrix[i][j] * B._matrix[i][j];
                }
                return result;
            }
            else
                throw new Exception("When calculating the Hadamard product of two Matrices, the dimensions of both must be equal.");
        }

        /// <summary>
        /// Creates and returns a shallow copy.
        /// </summary>
        public Matrix Clone()
        {
            Matrix result = new Matrix(this.Columns, this.Rows);
            for (int i = 0; i < this.Columns; i += 1)
            {
                for (int j = 0; j < this.Rows; j += 1)
                    result._matrix[i][j] = _matrix[i][j];
            }
            return result;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
