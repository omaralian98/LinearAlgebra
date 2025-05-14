using System.Text;

namespace Presentation;

public static class ExtensionMethods
{
    public static T[][]? ConvertMatrix<T>(this T[,]? matrix)
    {
        if (matrix is null)
        {
            return null;
        }
        try
        {
            int rowsCount = matrix.GetLength(0);
            int columnsCount = matrix.GetLength(1);
            T[][] result = new T[rowsCount][];

            for (int i = 0; i < rowsCount; i++)
            {
                result[i] = new T[columnsCount];
                for (int j = 0; j < columnsCount; j++)
                {
                    result[i][j] = matrix[i, j];
                }
            }
            return result;
        }
        catch
        {
        }
        return default;
    }


    public static Result<Fraction[][]> ConvertToFractionJagged(this List<List<string>> matrix)
    {
        int rowsCount = matrix.Count;
        Fraction[][] result = new Fraction[rowsCount][];
        for (int i = 0; i < rowsCount; i++)
        {
            int columnsCount = matrix[i].Count;
            result[i] = new Fraction[columnsCount];
            for (int j = 0; j < columnsCount; j++)
            {
                try
                {
                    result[i][j] = matrix[i][j];
                }
                catch (Exception ex)
                {
                    return new Error($"{nameof(ConvertToFractionJagged)}", ex.Message);
                }
            }
        }
        return result;
    }

    public static Result<Fraction[,]> ConvertToFraction(this List<List<string>> matrix)
    {
        if (matrix.Count == 0)
        {
            return new Fraction[,] { };
        }

        int rowsCount = matrix.Count;
        int columnsCount = matrix[0].Count;
        Fraction[,] result = new Fraction[rowsCount, columnsCount];
        for (int i = 0; i < rowsCount; i++)
        {
            if (matrix[i].Count != columnsCount)
            {
                return new Error($"{nameof(ConvertToFraction)}", $"{nameof(matrix)} was inconsistent");
            }

            for (int j = 0; j < columnsCount; j++)
            {
                try
                {
                    result[i, j] = matrix[i][j];
                }
                catch (Exception ex)
                {
                    return new Error($"{nameof(ConvertToFractionJagged)}", ex.Message);
                }
            }
        }
        return result;
    }

    public static async Task<Result<List<List<string>>>> ConvertStringToMatrixAsync(this string? text, string? rowSeparator = null, string columnSeparator = " ")
    {
        return await Task.Run(() =>
        {
            List<List<string>> Matrix = [];

            if (text is not null)
            {
                try
                {
                    var lines = text.Split(rowSeparator ?? Environment.NewLine);

                    int rowsCount = lines.Length;
                    int columnsCount = 1;

                    UpdateMatrixRows(Matrix, rowsCount, columnsCount);

                    for (int i = 0; i < rowsCount; i++)
                    {
                        var row = lines[i].Split(columnSeparator);
                        int newcolumnsCount = row.Length;
                        if (i == 0 || newcolumnsCount > columnsCount)
                        {
                            UpdateMatrixColumns(Matrix, rowsCount, newcolumnsCount);
                            columnsCount = newcolumnsCount;
                        }
                        for (int j = 0; j < newcolumnsCount; j++)
                        {
                            Matrix[i][j] = row[j];
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Result<List<List<string>>>.Failure((Error)ex);
                }
            }
            return Matrix;
        });
    }
    public static Result<List<List<string>>> ConvertStringToMatrix(this string? text, string? rowSeparator = null, string columnSeparator = " ")
    {
        List<List<string>> Matrix = [];

        if (text is not null)
        {
            try
            {
                var lines = text.Split(rowSeparator ?? Environment.NewLine);

                int rowsCount = lines.Length;
                int columnsCount = 1;

                UpdateMatrixRows(Matrix, rowsCount, columnsCount);

                for (int i = 0; i < rowsCount; i++)
                {
                    var row = lines[i].Split(columnSeparator);
                    int newcolumnsCount = row.Length;
                    if (i == 0 || newcolumnsCount > columnsCount)
                    {
                        UpdateMatrixColumns(Matrix, rowsCount, newcolumnsCount);
                        columnsCount = newcolumnsCount;
                    }
                    for (int j = 0; j < newcolumnsCount; j++)
                    {
                        Matrix[i][j] = row[j];
                    }
                }
            }
            catch (Exception ex)
            {
                return Result<List<List<string>>>.Failure((Error)ex);
            }
        }
        return Matrix;
    }


    public static void UpdateMatrixRows(this List<List<string>> Matrix, int Rows, int Columns)
    {
        int difference = Rows - Matrix.Count;

        if (difference > 0)
        {
            for (int i = 0; i < difference; i++)
            {
                Matrix.Add(new List<string>(new string[Columns]));
            }
        }
        else if (difference < 0)
        {
            for (int i = 0; i < -difference; i++)
            {
                Matrix.RemoveAt(Matrix.Count - 1);
            }
        }
    }

    public static void UpdateMatrixColumns(this List<List<string>> Matrix, int Rows, int Columns)
    {
        if (Matrix.Count == 0)
            return;

        int currentColumnCount = Matrix[0].Count;
        int difference = Columns - currentColumnCount;

        if (difference > 0)
        {
            foreach (var row in Matrix)
            {
                for (int j = 0; j < difference; j++)
                {
                    row.Add(string.Empty);
                }
            }
        }
        else if (difference < 0)
        {
            foreach (var row in Matrix)
            {
                for (int j = 0; j < -difference; j++)
                {
                    row.RemoveAt(row.Count - 1);
                }
            }
        }
    }

    public static async Task<string> ConvertMatrixToStringAsync(this List<List<string>> Matrix, string? rowSeparator = null, string columnSeparator = " ")
    {
        return await Task.Run(() =>
        {
            StringBuilder text = new();
            rowSeparator ??= Environment.NewLine;

            for (int i = 0; i < Matrix.Count; i++)
            {
                text.AppendJoin(columnSeparator, Matrix[i]);
                if (i + 1 != Matrix.Count)
                {
                    text.Append(rowSeparator);
                }
            }
            return text.ToString();
        });
    }

    public static string ConvertMatrixToString(this List<List<string>> Matrix, string? rowSeparator = null, string columnSeparator = " ")
    {
        StringBuilder text = new();
        rowSeparator ??= Environment.NewLine;

        for (int i = 0; i < Matrix.Count; i++)
        {
            text.AppendJoin(columnSeparator, Matrix[i]);
            if (i + 1 != Matrix.Count)
            {
                text.Append(rowSeparator);
            }
        }
        return text.ToString();
    }
}