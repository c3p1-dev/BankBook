#include <utility>

#include "data/account.h"

// account class implementation
std::uint64_t account::id() const
{
    return id_;
}
void account::id(std::uint64_t account_id)
{
    id_ = account_id;
}

std::int64_t account::initial_balance() const
{
    return initial_balance_;
}
void account::initial_balance(std::int64_t cents)
{
    initial_balance_ = cents;
}

const std::string& account::code() const
{
    return code_;
}
void account::code(std::string account_code)
{
    code_ = std::move(account_code);
}

const std::string& account::name() const
{
    return name_;
}
void account::name(std::string account_name)
{
    name_ = std::move(account_name);
}

const std::optional<std::string>& account::bank() const
{
    return bank_;
}
void account::bank(std::optional<std::string> bank_name)
{
    bank_ = std::move(bank_name);
}

const std::optional<std::string>& account::description() const
{
    return description_;
}
void account::description(std::optional<std::string> account_description)
{
    description_ = std::move(account_description);
}

const std::optional<std::string>& account::iban() const
{
    return iban_;
}
void account::iban(std::optional<std::string> account_iban)
{
    iban_ = std::move(account_iban);
}

const std::optional<std::string>& account::swift() const
{
    return swift_;
}
void account::swift(std::optional<std::string> account_swift)
{
    swift_ = std::move(account_swift);
}

const std::optional<std::string>& account::url() const
{
    return url_;
}
void account::url(std::optional<std::string> account_url)
{
    url_ = std::move(account_url);
}

std::optional<std::chrono::system_clock::time_point> account::locked_at() const
{
    return locked_at_;
}
void account::locked_at(std::optional<std::chrono::system_clock::time_point> lock_time)
{
    locked_at_ = lock_time;
}