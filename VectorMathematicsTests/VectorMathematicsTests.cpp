// VectorMathematicsTests.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include "../VectorMathematics/VectorMath.h"
#include <cassert>
#include <iostream>
#include <cmath>


// Assert == conditions
// Helper FloatEquals -> we need this to compare 
constexpr float EPSILON = 1e-6f;

bool FloatEquals(float a, float b)
{
    return std::abs(a - b) < EPSILON;
}

//Helpers: PrintVec3, PrintFloat and PrintBool -> we need this to print values
void PrintVec3(const char* label, Vec3 v)
{
    std::cout << label << ": (" << v.x << ", " << v.y << ", " << v.z << ")\n";
}
void PrintFloat(const char* label, float v)
{
    std::cout << label << ": " << v << "\n";
}
void PrintBool(const char* label, bool v)
{
    std::cout << label << ": " << (v ? "true" : "false") << "\n";
}

// We need to declare the function before using it
void TestVector3();

int main()
{
    TestVector3();

    ///
    std::cout << "ALL TESTS PASSED: OK\n";
    std::cin.get();
}

void TestVector3() {

    std::cout << "---- Vector3 Tests ----\n\n";

    Vec3 added = Vector3Add({ 1, 2, 3 }, { 4, 5, 6 });
    assert(added.x == 5 && added.y == 7 && added.z == 9);
    PrintVec3("Add", added);

    Vec3 sub = Vector3Subtraction({ 5, 5, 5 }, { 2, 3, 4 });
    assert(sub.x == 3 && sub.y == 2 && sub.z == 1);
    PrintVec3("Sub", sub);

    Vec3 scale = Vector3Scale({ 1, 2, 3 }, 2.0f);
    assert(scale.x == 2 && scale.y == 4 && scale.z == 6);
    PrintVec3("Scale", scale);

    Vec3 cross = Vector3Cross({ 1, 0, 0 }, { 0, 1, 0 });
    assert(cross.x == 0 && cross.y == 0 && cross.z == 1);
    PrintVec3("Cross", cross);

    float mag = Vector3Magnitude({ 3, 4, 0 });
    assert(FloatEquals(mag, 5.0f));
    PrintFloat("Magnitude", mag);

    float magSq = Vector3MagnitudSquared({ 3, 4, 0 });
    assert(FloatEquals(magSq, 25.0f));
    PrintFloat("MagnitudeSquared", magSq);

    float dist = Vector3Distance({ 0, 0, 0 }, { 3, 4, 0 });
    assert(FloatEquals(dist, 5.0f));
    PrintFloat("Distance", dist);

    float dot = Vector3Dot({ 1, 0, 0 }, { 0, 1, 0 });
    assert(FloatEquals(dot, 0.0f));
    PrintFloat("Dot", dot);

    Vec3 norm = Vector3Normalize({ 10, 0, 0 });
    assert(FloatEquals(norm.x, 1.0f));
    assert(FloatEquals(norm.y, 0.0f));
    assert(FloatEquals(norm.z, 0.0f));
    PrintVec3("Normalize", norm);

    Vec3 lerp = Vector3Lerp({ 0, 0, 0 }, { 10, 0, 0 }, 0.5f);
    assert(FloatEquals(lerp.x, 5.0f));
    assert(FloatEquals(lerp.y, 0.0f));
    assert(FloatEquals(lerp.z, 0.0f));
    PrintVec3("Lerp", lerp);

    Vec3 div = Vector3Divide({ 2,4,6 }, 2.0f);
    assert(div.x == 1 && div.y == 2 && div.z == 3);
    PrintVec3("Divide", div);

    float distSq = Vector3DistanceSquared({ 0,0,0 }, { 3,4,0 });
    assert(FloatEquals(distSq, 25.0f));
    PrintFloat("DistanceSquared", distSq);

    bool isZero = Vector3IsZero({ 0.0000001f, 0, 0 }, EPSILON);
    assert(isZero);
    PrintBool("IsZero", isZero);

    Vec3 slerp = Vector3Slerp({ 1,0,0 }, { 0,1,0 }, 0.5f);
    assert(FloatEquals(Vector3Magnitude(slerp), 1.0f));
    PrintVec3("Slerp", slerp);

    float angle = Vector3Angle({ 1,0,0 }, { 0,1,0 });
    assert(FloatEquals(angle, 3.14159265f / 2.0f));
    PrintFloat("Angle (rad)", angle);

    float scalarProj = Vector3ScalarProjection({ 2,0,0 }, { 1,0,0 });
    assert(FloatEquals(scalarProj, 2.0f));
    PrintFloat("ScalarProjection", scalarProj);

    Vec3 vectorProj = Vector3VectorProjection({ 2,2,0 }, { 1,0,0 });
    assert(FloatEquals(vectorProj.x, 2.0f));
    assert(FloatEquals(vectorProj.y, 0.0f));
    PrintVec3("VectorProjection", vectorProj);

    Vec3 reflect = Vector3Reflect({ 1,-1,0 }, { 0,1,0 });
    assert(FloatEquals(reflect.x, 1.0f));
    assert(FloatEquals(reflect.y, 1.0f));
    PrintVec3("Reflect", reflect);

    Vec3 clamp = Vector3ClampMagnitude({ 10,0,0 }, 5.0f);
    assert(FloatEquals(Vector3Magnitude(clamp), 5.0f));
    PrintVec3("ClampMagnitude", clamp);

    Vec3 dir = Vector3DirectionTo({ 0,0,0 }, { 10,0,0 }, EPSILON);
    assert(FloatEquals(dir.x, 1.0f));
    PrintVec3("DirectionTo", dir);

    Vec3 move = Vector3MoveTowards({ 0,0,0 }, { 10,0,0 }, 3.0f);
    assert(FloatEquals(move.x, 3.0f));
    PrintVec3("MoveTowards", move);

    bool approx = Vector3Approximately({ 1,1,1 }, { 1.0000001f,1,1 }, EPSILON);
    assert(approx);
    PrintBool("Approximately", approx);

    PrintVec3("Zero", Vector3Zero());
    PrintVec3("One", Vector3One());
    PrintVec3("Right", Vector3Right());
    PrintVec3("Up", Vector3Up());
    PrintVec3("Forward", Vector3Forward());

    /*std::cout << added.x << ", " << added.y << ", " << added.z;
    std::cin.get();*/
}

// Run program: Ctrl + F5 or Debug > Start Without Debugging menu
// Debug program: F5 or Debug > Start Debugging menu

// Tips for Getting Started: 
//   1. Use the Solution Explorer window to add/manage files
//   2. Use the Team Explorer window to connect to source control
//   3. Use the Output window to see build output and other messages
//   4. Use the Error List window to view errors
//   5. Go to Project > Add New Item to create new code files, or Project > Add Existing Item to add existing code files to the project
//   6. In the future, to open this project again, go to File > Open > Project and select the .sln file
