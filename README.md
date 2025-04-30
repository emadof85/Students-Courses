
# 🎯 Session_6 - Clean Architecture ASP.NET Core Project

هذا المشروع مبني باستخدام مبدأ **Clean Architecture** لتقسيم المسؤوليات وضمان سهولة الصيانة والتوسع.

---

## 🏛️ بنية المشروع

يحتوي المشروع على أربع طبقات رئيسية:

```
Students-Courses/
│
├── Domain/         # الطبقة الأساسية - تحتوي على الكيانات والواجهات
│   ├── Entities/
│   └── IRepositories/
│
├── Application/    # طبقة منطق الأعمال والخدمات
│   ├── DTOs/
│   ├── IServices/
│   ├── Mapping/
│   └── Services/
│
├── Infrastructure/ # طبقة الوصول إلى البيانات (Data Access)
│   ├── Contexts/
│   ├── Migrations/
│   ├── Repositories/
│   ├── Seeds/
│   └── Authentication/
│
└── Presentation/   # طبقة الواجهة - ASP.NET Core Web API
    ├── Areas/
    ├── Controllers/
    ├── Models/
    ├── Views/ (إن وجدت مؤقتًا)
    └── Program.cs
```

---

## 🔗 العلاقات بين الطبقات (Project Dependencies)

| الطبقة         | تعتمد على                                  |
|----------------|--------------------------------------------|
| `Domain`       | ❌ لا تعتمد على أي طبقة                    |
| `Application`  | ✅ تعتمد فقط على: `Domain`                 |
| `Infrastructure`| ✅ تعتمد فقط على: `Domain`                 |
| `Presentation` | ✅ تعتمد على: `Application` و (📌 فقط في `Program.cs`) على `Infrastructure` |

> 🔥 مبدأ **Dependency Inversion** مطبق بالكامل: تعتمد الطبقات العليا على واجهات (Interfaces) وليس على الطبقات الدنيا بشكل مباشر.

---

## 📌 تسجيل الخدمات في `Program.cs`

يتم تسجيل جميع الخدمات من `Application` و `Infrastructure` في `Program.cs` كما يلي:

```csharp
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
```

✅ بهذا الشكل، يبقى الاعتماد على `Infrastructure` محصورًا في نقطة بدء المشروع فقط.

---

## 🧠 لماذا هذا التصميم مهم؟

- **مرونة عالية**: يمكن تغيير قاعدة البيانات، أو طريقة التنفيذ بسهولة دون التأثير على بقية الطبقات.
- **اختبارات وحدة أسهل**: يمكنك اختبار الطبقات العليا (مثل الخدمات) دون الحاجة للوصول الحقيقي لقاعدة البيانات.
- **فصل واضح للمهام**: يسهل العمل الجماعي وتوزيع المسؤوليات.

---

## ✨ المستقبل

- إمكانية دعم أكثر من مصدر بيانات.
- دعم واجهات مستخدم متعددة (Web, Mobile).
- دعم Microservices لاحقًا بسهولة.

---

🛠 تم تصميم هذا المشروع وفقًا لأفضل ممارسات هندسة البرمجيات الحديثة.
