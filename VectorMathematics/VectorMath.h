#pragma once

#ifndef EXPORT
	#define EXPORT __declspec(dllexport)
#endif //EXPORT

struct Vec2 {
	float x;
	float y;
};

struct Vec3 {
	float x;
	float y;
	float z;
};


extern "C" {

	// VECTOR 3D
	// Basic Operations
	EXPORT Vec3 Vector3Add(Vec3 a, Vec3 b);						// A + B
	EXPORT Vec3 Vector3Subtraction(Vec3 a, Vec3 b);				// A - B
	EXPORT Vec3 Vector3Scale(Vec3 a, float scale);				// V * k
	EXPORT Vec3 Vector3Divide(Vec3 a, float scalar);			// A / k

	// Magnitud and distance
	EXPORT float Vector3Magnitude(Vec3 v);						// |V|
	EXPORT float Vector3MagnitudSquared(Vec3 v);				// |V|^2
	EXPORT float Vector3Distance(Vec3 a, Vec3 b);				// |A - B|
	EXPORT float Vector3DistanceSquared(Vec3 a, Vec3 b);			// |A - B|^2

	// Dot and Cross
	EXPORT float Vector3Dot(Vec3 a, Vec3 b);					// A · B
	EXPORT Vec3 Vector3Cross(Vec3 a, Vec3 b);					// A × B

	// Normalization
	EXPORT Vec3 Vector3Normalize(Vec3 v);						// normalize(V)
	EXPORT bool Vector3IsZero(Vec3 v, float epsilon);			// |V| < epsilon

	// Interpolation
	EXPORT Vec3 Vector3Lerp(Vec3 a, Vec3 b, float t);			// linear interpolation
	EXPORT Vec3 Vector3Slerp(Vec3 a, Vec3 b, float t);			// spherical interpolation (unit vectors)

	// Angles and Projections
	EXPORT float Vector3Angle(Vec3 a, Vec3 b);					// angle in radians
	EXPORT float Vector3ScalarProjection(Vec3 a, Vec3 b);		// Scalar projection of A onto B
	EXPORT Vec3  Vector3VectorProjection(Vec3 a, Vec3 b);		// Vector projection of A onto B

	// Reflection amd Movement
	EXPORT Vec3  Vector3Reflect(Vec3 v, Vec3 normal);					// reflection
	EXPORT Vec3  Vector3ClampMagnitude(Vec3 v, float maxLen);			// clamp |V|
	EXPORT Vec3  Vector3DirectionTo(Vec3 from, Vec3 to, float epsilon);	// normalized direction
	EXPORT Vec3  Vector3MoveTowards(Vec3 current, Vec3 target, float maxDistance);

	// For Utility
	EXPORT bool  Vector3Approximately(Vec3 a, Vec3 b, float epsilon);	// |A - B| < epsilon
	
	// Constants
	EXPORT Vec3 Vector3Zero();		// (0,0,0)
	EXPORT Vec3 Vector3One();       // (1,1,1)
	EXPORT Vec3 Vector3Right();     // (1,0,0)
	EXPORT Vec3 Vector3Up();        // (0,1,0)
	EXPORT Vec3 Vector3Forward();   // (0,0,1)

	// VECTOR 2D
	// Basic Operations
	EXPORT Vec2 Vector2Add(Vec2 a, Vec2 b);
	EXPORT Vec2 Vector2Subtraction(Vec2 a, Vec2 b);
	EXPORT Vec2 Vector2Scale(Vec2 a, float scale);
	EXPORT Vec2 Vector2Divide(Vec2 a, float scalar);

	// Magnitude and Distance
	EXPORT float Vector2Magnitude(Vec2 v);
	EXPORT float Vector2MagnitudeSquared(Vec2 v);
	EXPORT float Vector2Distance(Vec2 a, Vec2 b);
	EXPORT float Vector2DistanceSquared(Vec2 a, Vec2 b);

	// Dot and Cross
	EXPORT float Vector2Dot(Vec2 a, Vec2 b);
	EXPORT float Vector2Cross(Vec2 a, Vec2 b); //in 2D, rreturns an scalar, is not a vector

	// Normalization
	EXPORT Vec2 Vector2Normalize(Vec2 v);
	EXPORT bool Vector2IsZero(Vec2 v, float epsilon);

	// Interpolation
	EXPORT Vec2 Vector2Lerp(Vec2 a, Vec2 b, float t);
	EXPORT Vec2 Vector2Slerp(Vec2 a, Vec2 b, float t);

	// Angles and Projections
	EXPORT float Vector2Angle(Vec2 a, Vec2 b);
	EXPORT float Vector2ScalarProjection(Vec2 a, Vec2 b);
	EXPORT Vec2  Vector2VectorProjection(Vec2 a, Vec2 b);

	// Reflection and Movement
	EXPORT Vec2  Vector2Reflect(Vec2 v, Vec2 normal);
	EXPORT Vec2  Vector2ClampMagnitude(Vec2 v, float maxLen);
	EXPORT Vec2  Vector2DirectionTo(Vec2 from, Vec2 to, float epsilon);
	EXPORT Vec2  Vector2MoveTowards(Vec2 current, Vec2 target, float maxDistance);

	// Utility
	EXPORT bool  Vector2Approximately(Vec2 a, Vec2 b, float epsilon);

	// Constants
	EXPORT Vec2 Vector2Zero();
	EXPORT Vec2 Vector2One();
	EXPORT Vec2 Vector2Right();
	EXPORT Vec2 Vector2Up();
}

//Vec3 VectorAdd(Vec3 a, Vec3 b);