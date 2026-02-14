//Always include the pch.h FIRST in every CPP file where you need it (if your project is setup to use procompiled header files)
#include "pch.h"

// AND then you can include your own items
#include "VectorMath.h"
#include <cmath>

/*extern "C" __declspec(dllexport) Vec3 VectorAdd(Vec3 a, Vec3 b)
{
	return { a.x * b.x, a.y * b.y, a.z * b.z };
}*/

extern "C"
{

	// VECTOR 3D
	EXPORT Vec3 Vector3Add(Vec3 a, Vec3 b) {
			return { a.x + b.x, a.y + b.y, a.z + b.z };
	}

	EXPORT Vec3 Vector3Subtraction(Vec3 a, Vec3 b) {
		return { a.x - b.x, a.y - b.y, a.z - b.z };
	}

	EXPORT Vec3 Vector3Scale(Vec3 a, float scale) {
		return { a.x * scale, a.y * scale, a.z * scale };
	}

	EXPORT Vec3 Vector3Cross(Vec3 a, Vec3 b) {
		return {
			a.y * b.z - a.z * b.y,
			a.z * b.x - a.x * b.z,
			a.x * b.y - a.y * b.x
		};
	}

	EXPORT float Vector3Magnitude(Vec3 v) {
		return std::sqrt(v.x * v.x + v.y * v.y + v.z * v.z);
	}

	EXPORT float Vector3MagnitudSquared(Vec3 v) {
		return v.x * v.x + v.y * v.y + v.z * v.z;
	}

	EXPORT float Vector3Distance(Vec3 a, Vec3 b) {
		float dx = b.x - a.x;
		float dy = b.y - a.y;
		float dz = b.z - a.z;

		return std::sqrt(dx * dx + dy * dy + dz * dz);
	}

	EXPORT float Vector3Dot(Vec3 a, Vec3 b) {
		return a.x * b.x + a.y * b.y + a.z * b.z;
	}

	EXPORT Vec3 Vector3Normalize(Vec3 v) {
		float mag = Vector3Magnitude(v);

		if (mag == 0.0f)
			return { 0.0f, 0.0f, 0.0f };

		return {
			v.x / mag,
			v.y / mag,
			v.z / mag
		};
	}

	EXPORT Vec3 Vector3Lerp(Vec3 a, Vec3 b, float t) {
		return {
			a.x + (b.x - a.x) * t,
			a.y + (b.y - a.y) * t,
			a.z + (b.z - a.z) * t
		};
	}

	EXPORT Vec3 Vector3Divide(Vec3 a, float scalar)
	{
		if (std::abs(scalar) < 1e-6f)
			return { 0.0f, 0.0f, 0.0f };

		return { a.x / scalar, a.y / scalar, a.z / scalar };
	}

	EXPORT float Vector3DistanceSquared(Vec3 a, Vec3 b)
	{
		float dx = b.x - a.x;
		float dy = b.y - a.y;
		float dz = b.z - a.z;

		return dx * dx + dy * dy + dz * dz;
	}

	EXPORT bool Vector3IsZero(Vec3 v, float epsilon)
	{
		return Vector3MagnitudSquared(v) < epsilon * epsilon;
	}

	EXPORT Vec3 Vector3Slerp(Vec3 a, Vec3 b, float t)
	{
		float dot = Vector3Dot(a, b);
		dot = std::fmax(-1.0f, std::fmin(dot, 1.0f));

		float theta = std::acos(dot) * t;

		Vec3 relative = Vector3Normalize(
			Vector3Subtraction(b, Vector3Scale(a, dot))
		);

		return Vector3Add(
			Vector3Scale(a, std::cos(theta)),
			Vector3Scale(relative, std::sin(theta))
		);
	}

	EXPORT float Vector3Angle(Vec3 a, Vec3 b)
	{
		float magA = Vector3Magnitude(a);
		float magB = Vector3Magnitude(b);

		if (magA < 1e-6f || magB < 1e-6f)
			return 0.0f;

		float cosTheta = Vector3Dot(a, b) / (magA * magB);
		cosTheta = std::fmax(-1.0f, std::fmin(cosTheta, 1.0f));

		return std::acos(cosTheta);
	}

	EXPORT float Vector3ScalarProjection(Vec3 a, Vec3 b)
	{
		float magB = Vector3Magnitude(b);
		if (magB < 1e-6f)
			return 0.0f;

		return Vector3Dot(a, b) / magB;
	}

	EXPORT Vec3 Vector3VectorProjection(Vec3 a, Vec3 b)
	{
		float magBSq = Vector3MagnitudSquared(b);
		if (magBSq < 1e-6f)
			return { 0.0f, 0.0f, 0.0f };

		float scale = Vector3Dot(a, b) / magBSq;
		return Vector3Scale(b, scale);
	}

	EXPORT Vec3 Vector3Reflect(Vec3 v, Vec3 normal)
	{
		float dot = Vector3Dot(v, normal);
		return Vector3Subtraction(v, Vector3Scale(normal, 2.0f * dot));
	}

	EXPORT Vec3 Vector3ClampMagnitude(Vec3 v, float maxLen)
	{
		float mag = Vector3Magnitude(v);
		if (mag <= maxLen)
			return v;

		return Vector3Scale(Vector3Normalize(v), maxLen);
	}

	EXPORT Vec3 Vector3DirectionTo(Vec3 from, Vec3 to, float epsilon)
	{
		Vec3 dir = Vector3Subtraction(to, from);

		if (Vector3MagnitudSquared(dir) < epsilon * epsilon)
			return { 0.0f, 0.0f, 0.0f };

		return Vector3Normalize(dir);
	}

	EXPORT Vec3 Vector3MoveTowards(Vec3 current, Vec3 target, float maxDistance)
	{
		Vec3 delta = Vector3Subtraction(target, current);
		float dist = Vector3Magnitude(delta);

		if (dist <= maxDistance || dist < 1e-6f)
			return target;

		return Vector3Add(
			current,
			Vector3Scale(delta, maxDistance / dist)
		);
	}

	EXPORT bool Vector3Approximately(Vec3 a, Vec3 b, float epsilon)
	{
		return Vector3DistanceSquared(a, b) < epsilon * epsilon;
	}

	EXPORT Vec3 Vector3Zero() { return { 0.0f, 0.0f, 0.0f }; }
	EXPORT Vec3 Vector3One() { return { 1.0f, 1.0f, 1.0f }; }
	EXPORT Vec3 Vector3Right() { return { 1.0f, 0.0f, 0.0f }; }
	EXPORT Vec3 Vector3Up() { return { 0.0f, 1.0f, 0.0f }; }
	EXPORT Vec3 Vector3Forward() { return { 0.0f, 0.0f, 1.0f }; }




	// VECTOR 2D
	EXPORT Vec2 Vector2Add(Vec2 a, Vec2 b)
	{
		return { a.x + b.x, a.y + b.y };
	}

	EXPORT Vec2 Vector2Subtraction(Vec2 a, Vec2 b)
	{
		return { a.x - b.x, a.y - b.y };
	}

	EXPORT Vec2 Vector2Scale(Vec2 a, float scale)
	{
		return { a.x * scale, a.y * scale };
	}

	EXPORT Vec2 Vector2Divide(Vec2 a, float scalar)
	{
		if (std::abs(scalar) < 1e-6f)
			return { 0.0f, 0.0f };

		return { a.x / scalar, a.y / scalar };
	}

	EXPORT float Vector2Magnitude(Vec2 v)
	{
		return std::sqrt(v.x * v.x + v.y * v.y);
	}

	EXPORT float Vector2MagnitudeSquared(Vec2 v)
	{
		return v.x * v.x + v.y * v.y;
	}

	EXPORT float Vector2Distance(Vec2 a, Vec2 b)
	{
		float dx = b.x - a.x;
		float dy = b.y - a.y;
		return std::sqrt(dx * dx + dy * dy);
	}

	EXPORT float Vector2DistanceSquared(Vec2 a, Vec2 b)
	{
		float dx = b.x - a.x;
		float dy = b.y - a.y;
		return dx * dx + dy * dy;
	}

	EXPORT float Vector2Dot(Vec2 a, Vec2 b)
	{
		return a.x * b.x + a.y * b.y;
	}

	// 2D Cross returns scalar
	EXPORT float Vector2Cross(Vec2 a, Vec2 b)
	{
		return a.x * b.y - a.y * b.x;
	}

	EXPORT Vec2 Vector2Normalize(Vec2 v)
	{
		float mag = Vector2Magnitude(v);
		if (mag < 1e-6f)
			return { 0.0f, 0.0f };

		return { v.x / mag, v.y / mag };
	}

	EXPORT bool Vector2IsZero(Vec2 v, float epsilon)
	{
		return Vector2MagnitudeSquared(v) < epsilon * epsilon;
	}

	EXPORT Vec2 Vector2Lerp(Vec2 a, Vec2 b, float t)
	{
		return {
			a.x + (b.x - a.x) * t,
			a.y + (b.y - a.y) * t
		};
	}

	EXPORT Vec2 Vector2Slerp(Vec2 a, Vec2 b, float t)
	{
		float dot = Vector2Dot(a, b);
		dot = std::fmax(-1.0f, std::fmin(dot, 1.0f));

		float theta = std::acos(dot) * t;

		Vec2 relative = Vector2Normalize(
			Vector2Subtraction(b, Vector2Scale(a, dot))
		);

		return Vector2Add(
			Vector2Scale(a, std::cos(theta)),
			Vector2Scale(relative, std::sin(theta))
		);
	}

	EXPORT float Vector2Angle(Vec2 a, Vec2 b)
	{
		float magA = Vector2Magnitude(a);
		float magB = Vector2Magnitude(b);

		if (magA < 1e-6f || magB < 1e-6f)
			return 0.0f;

		float cosTheta = Vector2Dot(a, b) / (magA * magB);
		cosTheta = std::fmax(-1.0f, std::fmin(cosTheta, 1.0f));

		return std::acos(cosTheta);
	}

	EXPORT float Vector2ScalarProjection(Vec2 a, Vec2 b)
	{
		float magB = Vector2Magnitude(b);
		if (magB < 1e-6f)
			return 0.0f;

		return Vector2Dot(a, b) / magB;
	}

	EXPORT Vec2 Vector2VectorProjection(Vec2 a, Vec2 b)
	{
		float magBSq = Vector2MagnitudeSquared(b);
		if (magBSq < 1e-6f)
			return { 0.0f, 0.0f };

		float scale = Vector2Dot(a, b) / magBSq;
		return Vector2Scale(b, scale);
	}

	EXPORT Vec2 Vector2Reflect(Vec2 v, Vec2 normal)
	{
		float dot = Vector2Dot(v, normal);
		return Vector2Subtraction(v, Vector2Scale(normal, 2.0f * dot));
	}

	EXPORT Vec2 Vector2ClampMagnitude(Vec2 v, float maxLen)
	{
		float mag = Vector2Magnitude(v);
		if (mag <= maxLen)
			return v;

		return Vector2Scale(Vector2Normalize(v), maxLen);
	}

	EXPORT Vec2 Vector2DirectionTo(Vec2 from, Vec2 to, float epsilon)
	{
		Vec2 dir = Vector2Subtraction(to, from);

		if (Vector2MagnitudeSquared(dir) < epsilon * epsilon)
			return { 0.0f, 0.0f };

		return Vector2Normalize(dir);
	}

	EXPORT Vec2 Vector2MoveTowards(Vec2 current, Vec2 target, float maxDistance)
	{
		Vec2 delta = Vector2Subtraction(target, current);
		float dist = Vector2Magnitude(delta);

		if (dist <= maxDistance || dist < 1e-6f)
			return target;

		return Vector2Add(
			current,
			Vector2Scale(delta, maxDistance / dist)
		);
	}

	EXPORT bool Vector2Approximately(Vec2 a, Vec2 b, float epsilon)
	{
		return Vector2DistanceSquared(a, b) < epsilon * epsilon;
	}

	// Constants
	EXPORT Vec2 Vector2Zero() { return { 0.0f, 0.0f }; }
	EXPORT Vec2 Vector2One() { return { 1.0f, 1.0f }; }
	EXPORT Vec2 Vector2Right() { return { 1.0f, 0.0f }; }
	EXPORT Vec2 Vector2Up() { return { 0.0f, 1.0f }; }

}