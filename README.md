# Linear Algebra

A modern refactor and abstraction of an old C# + WPF project into a clean, extensible, and testable .NET 8 solution. The goal is to provide a comprehensive linear algebra calculator that not only computes answers but also teaches students by showing step-by-step solutions.

Unlike many scattered online tools, this project aims to bring essential linear algebra functionality into one place, with clear pedagogy, consistent UX, and transparent math.

## Key Features

-   Matrix operations with step-by-step solutions:
    -   Addition and Subtraction
    -   Scalar Multiplication
    -   Matrix Multiplication
    -   Transpose
    -   Determinant
    -   Rank
    -   Row Echelon Form (REF) and Reduced Row Echelon Form (RREF)
    -   LU Factorization
    -   Inverse
    -   Power
    -   Trace
    -   Classify/solve linear systems
-   Exact arithmetic support via Fractions where possible for clarity and learning
-   Clean separation of concerns: core math library, presentation layer
-   KaTeX-powered rendering for readable equations and math expressions in the UI
-   Extensible architecture for adding new topics and steps

Planned/Upcoming:

-   Matrices, Determinants, and Linear Equations
    -   Cofactors of a matrix
    -   Adjugate of a matrix
    -   Transform a matrix to echelon form (enhanced workflows)
-   Vector Spaces and Subspaces
    -   Express a vector as a linear combination
    -   Linear dependence and independence
    -   Subspace spanned by a set of vectors
    -   Dimension
    -   Extract a basis from a spanning set
    -   Find a basis for a subspace
    -   Expand a set of vectors into a basis
    -   Find the coordinate vector
    -   Replace a vector in a basis
    -   Determine if a set of vectors is a basis for a subspace
    -   Compute the change of basis matrix
    -   Intersection of two subspaces
    -   Sum of two subspaces
    -   Determine if two subspaces are complementary
    -   Determine if two subspaces are equal
    -   Find a complementary subspace
-   Linear Transformations
    -   Matrix of a linear transformation
    -   Compute the image of a vector
    -   Kernel and image of a linear transformation
    -   Determine if kernel and image are complementary
    -   Classify a linear transformation
    -   Find the linear transformation from two sets of vectors
    -   Find the linear transformation from a spanning set for the image
    -   Add and scale linear transformations
    -   Compose two linear transformations
    -   Compute the inverse of a linear transformation
    -   Characteristic polynomial
    -   Eigenvalues and eigenvectors
    -   Diagonalization of a linear transformation
    -   Determine if a subspace is invariant


## Project Structure
-   LinearAlgebra/ (Class Library)
    -   Core math engine implementing linear algebra algorithms and exposing step-by-step result models.
-   Presentation/ (Blazor application)
    -   Blazor WebAssembly PWA UI for entering problems and visualizing solutions (with KaTeX-based rendering).
-   LinearConsole/ (For demo purposes)
    -   Minimal console driver to exercise algorithms without the UI
-   Unit/ (Tests)
    -   Contains unit tests for core functionality (expand as the project evolves)


## Getting Started

The Presentation project is a Blazor WebAssembly app with PWA support. You can use it directly in your browser by visiting: [https://omaralian98.github.io/LinearAlgebra/](https://omaralian98.github.io/LinearAlgebra/)


## How It Works

- The LinearAlgebra library implements algorithms in pure C# with precise types and step-capturing result objects. Each algorithm surfaces:
  - Final result
  - Intermediate matrices/states
  - Human-readable explanation of each transformation (e.g., row operations, pivoting, permutation matrices, etc.)
- The Presentation app binds to these results and renders them with KaTeX and Blazor components for clarity and accessibility.

## Design Principles

- Separation of concerns: math core is UI-agnostic; UI is presentation-only
- Pedagogy-first: every operation aims to teach, not just compute
- Determinism and reproducibility: steps are explicit and exportable
- Extensibility: easy to add new operations and new step renderers

## Roadmap

- Vectors and vector spaces (span, basis, dimension)
- Eigenvalues/eigenvectors, characteristic polynomials
- QR/Cholesky/SVD factorizations
- Numerical stability options and pivot strategy controls
- Export: copy/share LaTeX, image exports, and JSON steps
- Localization and accessibility improvements
- Keyboard-first UX, better mobile support
- More tutorials and inline tips per operation


## Notes on the Refactor

- This project stems from a legacy C# + WPF app that had grown organically and lacked clear abstraction boundaries
- The refactor:
  - Extracts a clean core math library (LinearAlgebra)
  - Adds a modern, cross-platform UI (Blazor)
  - Improves correctness, readability, and testability
  - Introduces explicit step outputs to aid learning
  
- This project specially the Presentation is not the final product but a mere show case.
