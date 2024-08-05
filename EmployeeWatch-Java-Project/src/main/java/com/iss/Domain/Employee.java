package com.iss.Domain;

import java.time.LocalDateTime;
import java.util.Objects;

/**
 * 
 */
public class Employee extends User {
    private final Grade grade;
    private final Float salary;
    private LocalDateTime presentTime = null;

    public Employee(Integer id, String name, String cnp, String username, Role role, Grade grade, Float salary) {
        super(id, name, cnp, username, role);
        this.grade = grade;
        this.salary = salary;
    }

    public Grade getGrade() {
        return grade;
    }

    public Float getSalary() {
        return salary;
    }

    public LocalDateTime getPresentTime() {
        return presentTime;
    }

    public void setPresentTime(LocalDateTime presentTime) {
        this.presentTime = presentTime;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        if (!super.equals(o)) return false;
        Employee employee = (Employee) o;
        return grade == employee.grade && Objects.equals(salary, employee.salary) && Objects.equals(presentTime, employee.presentTime);
    }

    @Override
    public int hashCode() {
        return Objects.hash(super.hashCode(), grade, salary, presentTime);
    }

    @Override
    public String toString() {
        return "Employee{" +
                "grade=" + grade +
                ", salary=" + salary +
                ", presentTime=" + presentTime +
                "} " + super.toString();
    }
}