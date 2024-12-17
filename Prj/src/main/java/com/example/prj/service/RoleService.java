package com.example.prj.service;

import com.example.prj.entity.Role;
import com.example.prj.repository.RoleRepository;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

// Аннотация @Service указывает, что этот класс является сервисом Spring,
// который содержит бизнес-логику.
@Service
public class RoleService {

    // Финальное поле roleRepository используется для работы с репозиторием RoleRepository.
    private final RoleRepository roleRepository;

    // Конструктор для внедрения зависимости RoleRepository.
    public RoleService(RoleRepository roleRepository) {
        this.roleRepository = roleRepository;
    }

    // Возвращает список всех ролей из базы данных.
    public List<Role> getAllRoles() {
        return roleRepository.findAll();
    }

    // Возвращает роль по её идентификатору, обёрнутую в Optional.
    public Optional<Role> getRoleById(Long id) {
        return roleRepository.findById(id);
    }

    // Сохраняет новую роль в базе данных.
    public Role saveRole(Role role) {
        return roleRepository.save(role);
    }

    // Обновляет существующую роль, если она найдена в базе данных.
    public Role updateRole(Long id, Role updatedRole) {
        return roleRepository.findById(id)
                .map(existingRole -> {
                    // Обновляет имя роли.
                    existingRole.setName(updatedRole.getName());
                    // Сохраняет изменения в базе данных.
                    return roleRepository.save(existingRole);
                })
                // Если роль не найдена, выбрасывается исключение.
                .orElseThrow(() -> new RuntimeException("Role not found"));
    }

    // Удаляет роль по её идентификатору.
    public void deleteById(Long id) {
        roleRepository.deleteById(id);
    }
}
