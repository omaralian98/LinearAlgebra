using LinearAlgebra;
using LinearAlgebra.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace Linear_API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class LinearController(ILogger<LinearController> logger) : ControllerBase
{

    private static bool IsNotCoherent<T, S>(T[,] matrix, S[] coefficient) =>
        matrix.GetLength(0) != coefficient.Length;

    [HttpGet]
    public IActionResult REF([Required][FromQuery]string[] a,[Optional][FromQuery]string[] b, [Required] int row)
    {
        Fraction[,] matrix = a.ConvertTo2D(row).GetFractions();
        if (b.Length != 0)
        {
            if (IsNotCoherent(matrix, b))
            {
                return BadRequest("Coefficient wasn't in correct format");
            }
            if (char.IsAsciiLetter(b[0][0]))
            {
                var coe = b.String2SpecialString();
                var result1 = Linear.REFAsString(matrix, coe);
                object oneD1 = new { matrix = result1.Item1, coefficient = result1.Item2 };
                return Ok(oneD1);
            }
            Fraction[] coefficient = b.GetFractions();
            var result = Linear.REFAsString(matrix, coefficient);
            object oneD = new { matrix = result.Item1, coefficient = result.Item2 };
            return Ok(oneD);
        }
        string[,] res = Linear.REFAsString(matrix, new CancellationToken());
        return Ok(res);
    }

    [HttpGet]
    public IActionResult REFAsDecimal([Required][FromQuery] string[] a, [Optional][FromQuery] string[] b, [Required] int row)
    {
        Fraction[,] matrix = a.ConvertTo2D(row).GetFractions();
        if (b.Length != 0)
        {
            if (IsNotCoherent(matrix, b))
            {
                return BadRequest("Coefficient wasn't in correct format");
            }
            if (char.IsAsciiLetter(b[0][0]))
            {
                var coe = b.String2SpecialString();
                var result1 = Linear.REFAsDecimalVariable(matrix, coe);
                object oneD1 = new { matrix = result1.Item1, coefficient = result1.Item2 };
                return Ok(oneD1);
            }
            Fraction[] coefficient = b.GetFractions();
            var result = Linear.REFAsDecimal(matrix, coefficient);
            object oneD = new { matrix = result.Item1, coefficient = result.Item2 };
            return Ok(oneD);
        }
        decimal[,] res = Linear.REFAsDecimal(matrix);
        return Ok(res);
    }

    [HttpGet]
    public IActionResult REFAsFraction([Required][FromQuery] string[] a, [Optional][FromQuery] string[] b, [Required] int row)
    {
        Fraction[,] matrix = a.ConvertTo2D(row).GetFractions();
        if (b.Length != 0)
        {
            if (IsNotCoherent(matrix, b))
            {
                return BadRequest("Coefficient wasn't in correct format");
            }
            if (char.IsAsciiLetter(b[0][0]))
            {
                var coe = b.String2SpecialString();
                var result1 = Linear.REFAsFractionVariable(matrix, coe);
                object oneD1 = new { matrix = result1.Item1, coefficient = result1.Item2 };
                return Ok(oneD1);
            }
            Fraction[] coefficient = b.GetFractions();
            var result = Linear.REFAsFraction(matrix, coefficient);
            object oneD = new { matrix = result.Item1, coefficient = result.Item2 };
            return Ok(oneD);
        }
        var res = Linear.REFAsFraction(matrix);
        return Ok(res);
    }

    [HttpGet]
    public IActionResult RREF([Required][FromQuery] string[] a, [Optional][FromQuery] string[] b, [Required] int row)
    {
        Fraction[,] matrix = a.ConvertTo2D(row).GetFractions();
        if (b.Length != 0)
        {
            if (IsNotCoherent(matrix, b))
            {
                return BadRequest("Coefficient wasn't in correct format");
            }
            if (char.IsAsciiLetter(b[0][0]))
            {
                var coe = b.String2SpecialString();
                var result1 = Linear.REFAsFractionVariable(matrix, coe);
                object oneD1 = new { matrix = result1.Item1, coefficient = result1.Item2 };
                return Ok(oneD1);
            }
            Fraction[] coefficient = b.GetFractions();
            var result = Linear.RREFAsString(matrix, coefficient);
            object oneD = new { matrix = result.Item1, coefficient = result.Item2 };
            return Ok(oneD);
        }
        var res = Linear.RREFAsString(matrix);
        return Ok(res);
    }

    [HttpGet]
    public IActionResult REFWithSteps([Required][FromQuery] string[] a, [Optional][FromQuery] string[] b, [Required] int row)
    {
        Fraction[,] matrix = a.ConvertTo2D(row).GetFractions();
        if (b.Length != 0)
        {
            if (IsNotCoherent(matrix, b))
            {
                return BadRequest("Coefficient wasn't in correct format");
            }
            if (char.IsAsciiLetter(b[0][0]))
            {
                SpecialString[] coe = b.String2SpecialString();
                var results1 = Linear.REFWithStringVariable(matrix, coe);
                return Ok(results1);
            }
            Fraction[] coefficient = b.GetFractions();
            var result = Linear.REFWithString(matrix, coefficient);
            return Ok(result);
        }
        var res = Linear.REFWithString(matrix);
        return Ok(res);
    }

    [HttpGet]
    public IActionResult RREFWithSteps([Required][FromQuery] string[] a, [Optional][FromQuery] string[] b, [Required] int row)
    {
        Fraction[,] matrix = a.ConvertTo2D(row).GetFractions();
        if (b.Length != 0)
        {
            if (IsNotCoherent(matrix, b))
            {
                return BadRequest("Coefficient wasn't in correct format");
            }
            if (char.IsAsciiLetter(b[0][0]))
            {
                SpecialString[] coe = b.String2SpecialString();
                var results1 = Linear.REFWithString(matrix, coe);
                return Ok(results1);
            }
            Fraction[] coefficient = b.GetFractions();
            var result = Linear.RREFWithString(matrix, coefficient);
            return Ok(result);
        }
        var res = Linear.RREFWithString(matrix);
        return Ok(res);
    }

    [HttpGet]
    [Route("{length}")]
    public IActionResult Generate(int length, bool @decimal = false, bool integer = false, decimal min = -9, decimal max = 9) => 
        Ok(
            @decimal ? 
            API_Helpers.API_GenerateDecimal(length, IntegersOnly: integer, min: min, max: max) :
            API_Helpers.API_GenerateString(length, IntegersOnly: integer, min: min, max: max)
          );
}
