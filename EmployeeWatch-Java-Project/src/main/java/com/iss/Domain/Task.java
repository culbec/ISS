package com.iss.Domain;

import java.time.LocalDateTime;
import java.util.*;

/**
 *
 */
public class Task extends Entity<Integer> {
    private final String description;
    private final LocalDateTime startTime;

    public Task(Integer id, String description, LocalDateTime startTime) {
        super(id);
        this.description = description;
        this.startTime = startTime;
    }

    public String getDescription() {
        return description;
    }

    public LocalDateTime getStartTime() {
        return startTime;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        if (!super.equals(o)) return false;
        Task task = (Task) o;
        return Objects.equals(description, task.description) && Objects.equals(startTime, task.startTime);
    }

    @Override
    public int hashCode() {
        return Objects.hash(super.hashCode(), description, startTime);
    }

    @Override
    public String toString() {
        return "Task{" +
                "description='" + description + '\'' +
                ", startTime=" + startTime +
                "} " + super.toString();
    }
}